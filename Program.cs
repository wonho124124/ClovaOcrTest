// See https://aka.ms/new-console-template for more information
using ClovaOcrTest.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ClovaOcrTest
{
    public class ClovaOcrTest
    {
        /// <summary>
        /// 현재 작업에 대한 정보 객체
        /// </summary>
        private static TaskData _taskData;

        static string Url = "https://ch9oki5xr3.apigw.ntruss.com/custom/v1/21285/499dde92aacf74f69e94985499caf9e5197a9ee3f213a972d4d4b07d9eb3baf5/general";
        static string SecretKey = "YUV4T0RBSm9heEJTVE1YTXJjZEV2Q1had3ZNRUtVQ3E=";
        static double InferConfidence = 0.7;

        /// <summary>
        /// Clova 통신객체
        /// </summary>
        private static CommunicationObj _currentRequestObj;

        public static bool MakeJsonData()
        {
            try
            {
                // Open CV 사용시에 TemPath 사용
                string filePath = _taskData.IsOpenCvUse == true ? _taskData.TempPath : _taskData.SrcImagePath;

                _currentRequestObj = new CommunicationObj
                {
                    version = "v2",
                    requestId = "gemini",
                    timestamp = 0,
                    images = new Image[1]
                    {
                        new Image
                        {
                            format = Path.GetExtension(_taskData.SrcImagePath).Replace(".", ""),
                            data = Helper.ImageToBase64(filePath),
                            name = Path.GetFileName(filePath)
                        }
                    }
                };
                return true;
            }
            catch (Exception ex)
            {
                ConsoleWriteLine(ResultType.Error, $"{ex.Message}");
                return false;
            }
        }

        private static void GetCommandArgs(string[] args)
        {
            _taskData.SrcImagePath = args[0];

            if (args.Length > 1)
            {
                _taskData.IsOpenCvUse = true;
                _taskData.IsDeleteTmpImageFile = Convert.ToBoolean(args[1]);
                _taskData.TempPath = Helper.GetImageTempPath(_taskData.SrcImagePath);
            }

            if (args.Length > 2)
            {
                _taskData.IsOpenCvUse = true;
                //? 뭔가 이상한데
                string[] arr = args[2].Split('|');
                if (arr.Length > 0)
                {
                    _taskData.SiteName = arr[0];
                }
                if (arr.Length > 1)
                {
                    _taskData.ProgramName = arr[1];
                }
            }
        }

        static void Main(string[] args)
        {
            _taskData = new TaskData();

            GetCommandArgs(args);

            try
            {
                var client = new RestClient(Url);
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-OCR-SECRET", SecretKey);

                var body = JsonConvert.SerializeObject(_currentRequestObj, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.Indented
                });

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                if (response.StatusCode == 0)
                {
                    ConsoleWriteLine(ResultType.Error, $"StatusCode = 0");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseObj = JsonConvert.DeserializeObject<CommunicationObj>(response.Content);
                    ParsingOcrText(responseObj);
                }
                else
                {
                    // 파라미터등 잘못 보냈을 경우 등
                    var responseObj = JsonConvert.DeserializeObject<FailResponseJson>(response.Content);
                    ConsoleWriteLine(ResultType.Error, responseObj.message);
                }
            }
            catch (Exception ex)
            {
                ConsoleWriteLine(ResultType.Error, ex.Message);
            }
        }

        public static void ParsingOcrText(CommunicationObj obj)
        {
            string result = string.Empty;
            try
            {
                foreach (var image in obj.images)
                {
                    // 결과값 
                    if (image.inferResult.ToUpper() != "SUCCESS")
                    {
                        throw new Exception("InferResult is False");
                    }
                    // 일치률 설정값보다 높은것만 파싱
                    foreach (var field in image.fields)
                    {
                        if (field.inferConfidence >= InferConfidence)
                        {
                            result += $"{field.inferText} ";
                        }
                    }
                }
                //ConsoleWriteLine(WriteType.Success, OCRHelper.StringtoBase64(result));
                ConsoleWriteLine(ResultType.Success, result);
            }
            catch (Exception ex)
            {
                ConsoleWriteLine(ResultType.Error, ex.Message);
            }
        }

        public static void ConsoleWriteLine(ResultType type, string msg)
        {
            string div = type == ResultType.Error ? "Error : " : string.Empty;
            Console.WriteLine($"{div}{msg}");
        }
    }
}