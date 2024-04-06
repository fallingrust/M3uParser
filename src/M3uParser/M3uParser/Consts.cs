namespace M3uParser
{
    public class Consts
    {
        /// <summary>
        ///  M3U 的文件头
        /// </summary>
        public const string EXTM3U = "#EXTM3U";
        /// <summary>
        /// 轨道信息和其他附加属性
        /// </summary>
        public const string EXTINF = "#EXTINF:";
        /// <summary>
        /// 最大段持续时间
        /// </summary>
        public const string EXT_X_TARGETDURATION = "#EXT-X-TARGETDURATION:";
        /// <summary>
        /// 作为演绎版一部分的变体流
        /// </summary>
        public const string EXT_X_STREAM_INF = "#EXT-X-STREAM-INF:";
        /// <summary>
        /// 媒体序列号
        /// </summary>
        public const string EXT_X_MEDIA_SEQUENCE = "#EXT-X-MEDIA-SEQUENCE:";
        /// <summary>
        /// 如何解密媒体段
        /// </summary>
        public const string EXT_X_KEY = "#EXT-X-KEY:";
        /// <summary>
        /// 媒体段的第一个样本与绝对日期和/或时间相关联
        /// </summary>
        public const string EXT_X_PROGRAM_DATE_TIME = "#EXT-X-PROGRAM-DATE-TIME:";
        public const string EXT_X_ALLOW_CATCH = "#EXT-X-ALLOW-CATCH:";
        /// <summary>
        /// 表示不再向文件添加媒体段
        /// </summary>
        public const string EXT_X_ENDLIST = "#EXT-X-ENDLIST";
        /// <summary>
        /// 表示前后媒体片段之间的不连续性
        /// </summary>
        public const string EXT_X_DISCONTINUITY = "#EXT-X-DISCONTINUITY:";
        /// <summary>
        /// 基于媒体及其服务器的文件的兼容性版本
        /// </summary>
        public const string EXT_X_VERSION = "#EXT-X-VERSION:";
        /// <summary>
        /// 播放列表的类型
        /// </summary>
        public const string EXT_X_PLAYLIST_TYPE = "#EXT-X-PLAYLIST-TYPE:";
        /// <summary>
        /// 播放列表的首选起点
        /// </summary>
        public const string EXT_X_START = "#EXT-X-START:";

        public const string PROGRAM_ID = "PROGRAM-ID";
        public const string BANDWIDTH = "BANDWIDTH";
       
        public const string CODECS = "CODECS";
    }
}
