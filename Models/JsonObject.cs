using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClovaOcrTest.Models
{
    /// <summary>
    /// 통신했으나, 결과가 실패인경우 사용하는 클래스
    /// </summary>
    public class FailResponseJson
    {
        public string code { get; set; }
        public string message { get; set; }
        public string path { get; set; }
        public string traceId { get; set; }
        public long timestamp { get; set; }
    }

    /// <summary>
    /// Request,Response에 사용
    /// </summary>
    public class CommunicationObj
    {
        /// <summary>
        /// 버전정보 v2 권장 (v1 ,v2) V2사용시 BoundingPoly 제공 ( X,Y 좌표값인듯)
        /// 필수값
        /// </summary>                
        public string version { get; set; }
        /// <summary>
        /// API 호출 UUID
        /// 필수값
        /// </summary>
        public string requestId { get; set; }
        /// <summary>
        /// API 호출 Timestamp 값
        /// 필수값
        /// </summary>
        public long timestamp { get; set; }

        public string resultType { get; set; }

        /// <summary>
        /// ko,ja,zh-TW
        /// 기본설정값으로 설정됌
        /// </summary>
        public string lang { get; set; }

        /// <summary>
        /// 배열이지만 현재는 이미지 한개만 지원됌
        /// </summary>
        public Image[] images { get; set; }
    }

    public class Image
    {
        /// <summary>
        /// jpg,jpeg,png,pdf,tiff 지원
        /// 필수값
        /// </summary>
        public string format { get; set; }

        /// <summary>
        /// data 또는 url 사용
        /// </summary>
        public string url { get; set; }
        public string data { get; set; }

        public string uid { get; set; }
        public string name { get; set; }
        public string inferResult { get; set; }
        public string message { get; set; }
        public Validationresult validationResult { get; set; }
        public Convertedimageinfo convertedImageInfo { get; set; }
        public Field[] fields { get; set; }
    }

    public class Validationresult
    {
        public string result { get; set; }
    }

    public class Convertedimageinfo
    {
        public int width { get; set; }
        public int height { get; set; }
        public int pageIndex { get; set; }
        public bool longImage { get; set; }
    }

    public class Field
    {
        public string valueType { get; set; }
        public Boundingpoly boundingPoly { get; set; }
        public string inferText { get; set; }
        public float inferConfidence { get; set; }
        public string type { get; set; }
        public bool lineBreak { get; set; }
    }

    public class Boundingpoly
    {
        public Vertex[] vertices { get; set; }
    }

    public class Vertex
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class MatchTemplateData
    {
        [JsonProperty(PropertyName = "site")]
        public string Site { get; set; }
        [JsonProperty(PropertyName = "similarity")]
        public double Similarity { get; set; }
        //[JsonProperty(PropertyName ="Resize")]
        //public ResizeCompareText Resize { get; set; }
        [JsonProperty(PropertyName = "breaking_news")]
        public List<SampleImageInfo> BreakingNews { get; set; }
        [JsonProperty(PropertyName = "presets")]
        public List<PresetInfo> Presets { get; set; }

    }

    public class PresetInfo
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "headline")]
        public SampleImageInfo Headline { get; set; }
        [JsonProperty(PropertyName = "preview")]
        public SampleImageInfo Preview { get; set; }
        [JsonProperty(PropertyName = "main")]
        public SampleImageInfo Main { get; set; }
    }

    public class SampleImageInfo
    {
        [JsonProperty(PropertyName = "image")]
        public List<string> ImagePath { get; set; }
        //[JsonProperty(PropertyName = "compare_position")]
        //public RectPosition compare_position { get; set; }
        [JsonProperty(PropertyName = "ocr_position")]
        public RectPosition OcrPosition { get; set; }
        [JsonProperty(PropertyName = "compareTextWidth")]
        public int CmpTextWidth { get; set; }
    }
    public class RectPosition
    {
        [JsonProperty(PropertyName = "x")]
        public int X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public int Y { get; set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
    }
}
