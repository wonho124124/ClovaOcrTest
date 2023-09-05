using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClovaOcrTest.Models
{
    public class TaskData
    {
        /// <summary>
        /// 원본 이미지 경로
        /// </summary>
        public string SrcImagePath { get; set; } = string.Empty;

        /// <summary>
        /// 뉴스 로고 경로
        /// </summary>
        public string MatchingLogoImagePath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "matchImageList.json");
        /// <summary>
        /// 임시파일 경로
        /// </summary>
        public string TempPath { get; set; } = string.Empty;
        /// <summary>
        /// Open Cv 사용유무
        /// </summary>
        public bool IsOpenCvUse { get; set; } = false;

        /// <summary>
        /// 템프파일 삭제 유무
        /// </summary>
        public bool IsDeleteTmpImageFile { get; set; } = false;

        /// <summary>
        /// ???
        /// </summary>
        public string SiteName { get; set; } = string.Empty;

        /// <summary>
        /// ???
        /// </summary>
        public string ProgramName { get; set; } = string.Empty;

        #region 이미지에 대한 정보
        public int ImageX { get; set; } = 0;
        public int ImageY { get; set; } = 0;

        public int ImageWidth { get; set; } = 0;

        public int ImageHeight { get; set; } = 0;

        #endregion
    }
}
