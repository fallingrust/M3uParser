using System.Collections.Generic;
using System.Linq;

namespace M3uParser
{
    public class PlayList
    {
        public List<PlayItem> Items { get; set; } = new List<PlayItem>();
        
        public int MediaSequence { get; set; }

        public int Version { get; set; }

        public bool AllowCache { get; set; }

        public int TargetDuration { get; set; }

        public bool MasterPlaylist { get; set; }
        public static bool TryParse(string m3u8, out PlayList? playList)
        {
            playList = null;
            var lines = m3u8.Split("#").Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => "#" + p.Replace("\r\n", ","));
            
            if (!lines.Any()) return false;
            if (!lines.First().StartsWith(Consts.EXTM3U)) return false;
            if (!lines.Last().StartsWith(Consts.EXT_X_ENDLIST)) return false;

            playList = new PlayList();

            #region 
            var versionItem = lines.FirstOrDefault(p => p.StartsWith(Consts.EXT_X_VERSION));
          
            if (versionItem != null)
            {
                var version = versionItem.Replace(Consts.EXT_X_VERSION, "");
                if (int.TryParse(version, out var v)) 
                    playList.Version = v;

            }

            var mediaSequenceItem = lines.FirstOrDefault(p => p.StartsWith(Consts.EXT_X_MEDIA_SEQUENCE));
            if (mediaSequenceItem != null)
            {
                var mediaSequence = mediaSequenceItem.Replace(Consts.EXT_X_MEDIA_SEQUENCE, "");
                if (int.TryParse(mediaSequence, out var seq))
                    playList.MediaSequence = seq;
            }

            var targetDurationItem = lines.FirstOrDefault(p => p.StartsWith(Consts.EXT_X_TARGETDURATION));
            if (targetDurationItem != null)
            {
                var targetDuration = targetDurationItem.Replace(Consts.EXT_X_TARGETDURATION, "");
                if (int.TryParse(targetDuration, out var maxDuration))
                    playList.TargetDuration = maxDuration;
            }

            playList.MasterPlaylist = lines.FirstOrDefault(p => p.StartsWith(Consts.EXT_X_STREAM_INF)) != null;
            #endregion


            if (playList.MasterPlaylist)
            {
                foreach (var line in lines)
                {
                    if (line.StartsWith(Consts.EXT_X_STREAM_INF))
                    {
                        var infos = line.Replace(Consts.EXT_X_STREAM_INF, "").Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
                        if (infos.Length > 0)
                        {
                            var item = new PlayItem();
                            for (int i = 0; i < infos.Length - 1; i++)
                            {
                                if (i == infos.Length - 1)
                                {
                                    item.Url = infos[i];
                                }
                                else
                                {
                                    var index = infos[i].IndexOf("=");
                                    if (index > 0)
                                    {
                                        var key = infos[i][..index];
                                        var value = infos[i][index..];
                                        item.ExtentionData.TryAdd(key, value);
                                    }
                                }
                            }
                            playList.Items.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (var line in lines)
                {
                    if (line.StartsWith(Consts.EXTINF))
                    {
                        var infos = line.Replace(Consts.EXTINF, "").Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
                        //标准 EXTINF
                        if (infos.Length == 2)
                        {
                            var item = new PlayItem();
                            if (double.TryParse(infos[0], out var duration))
                            {
                               item.Duration = duration;
                            }
                            item.Url = infos[^1];
                            playList.Items.Add(item);
                        }
                        //TVG
                        else if (infos.Length == 4 || infos.Length == 3)
                        {
                            var item = new PlayItem
                            {
                                Title = infos[^2],
                                Url = infos[^1]
                            };
                            if (double.TryParse(infos[0], out var duration))
                            {
                                item.Duration = duration;
                            }

                            var tags = infos[^3].Split(' ');
                            for (int i = 0; i < tags.Length; i++)
                            {
                                if (i == 0 && infos.Length == 3)
                                {
                                    if (double.TryParse(tags[i], out duration))
                                    {
                                        item.Duration = duration;
                                    }
                                }
                                else
                                {
                                    var index = tags[i].IndexOf("=");
                                    if (index > 0)
                                    {
                                        var key = tags[i][..index];
                                        var value = tags[i][index..];
                                        item.ExtentionData.TryAdd(key, value);
                                    }
                                }

                            }

                            playList.Items.Add(item);
                        }
                    }
                }
            }
            


            return true;
        }
    }
}
