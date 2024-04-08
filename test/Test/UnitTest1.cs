using M3uParser;

namespace Test
{
    public class UnitTest1
    {
       
        [InlineData("#EXTM3U\r\n#EXT-X-VERSION:3\r\n#EXT-X-TARGETDURATION:3\r\n#EXT-X-MEDIA-SEQUENCE:3\r\n#EXTINF:3.000000 \r\n/Users/renhui/Desktop/test/output3.ts\r\n#EXTINF:3.000000,\r\n/Users/renhui/Desktop/test/output4.ts\r\n#EXTINF:3.000000,\r\n/Users/renhui/Desktop/test/output5.ts\r\n#EXTINF:3.000000,\r\n/Users/renhui/Desktop/test/output6.ts\r\n#EXTINF:1.000000,\r\n/Users/renhui/Desktop/test/output7.ts\r\n#EXT-X-ENDLIST")]
        [Theory]
        public void TestMediaPlayList(string m3u)
        {
            var result = PlayList.TryParse(m3u, out var list);
            Assert.True(result);
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Version);
            Assert.False(list.MasterPlaylist);
           
        }


        [InlineData("#EXTM3U\r\n#EXT-X-STREAM-INF:BANDWIDTH=150000,RESOLUTION=416x234,CODECS=\"avc1.42e00a,mp4a.40.2\"\r\nhttp://example.com/low/index.m3u8\r\n#EXT-X-STREAM-INF:BANDWIDTH=240000,RESOLUTION=416x234,CODECS=\"avc1.42e00a,mp4a.40.2\"\r\nhttp://example.com/lo_mid/index.m3u8\r\n#EXT-X-STREAM-INF:BANDWIDTH=440000,RESOLUTION=416x234,CODECS=\"avc1.42e00a,mp4a.40.2\"\r\nhttp://example.com/hi_mid/index.m3u8\r\n#EXT-X-STREAM-INF:BANDWIDTH=640000,RESOLUTION=640x360,CODECS=\"avc1.42e00a,mp4a.40.2\"\r\nhttp://example.com/high/index.m3u8\r\n#EXT-X-STREAM-INF:BANDWIDTH=64000,CODECS=\"mp4a.40.5\"\r\nhttp://example.com/audio/index.m3u8\r\n#EXT-X-ENDLIST")]
        [Theory]
        public void TestMasterPlayList(string m3u)
        {
            var result = PlayList.TryParse(m3u, out var list);
            Assert.True(result);
            Assert.NotNull(list);
            Assert.True(list.MasterPlaylist);           
            Assert.Equal(5, list.Items.Count);
        }

        [InlineData("#EXTM3U\r\n#EXTINF:-1 tvg-name=\"CCTV3\" tvg-logo=\"https://epg.112114.xyz/logo/cctv3.png\" group-title=\"•央视「IPV6」\",CCTV3「IPV6」\r\nhttp://[2409:8087:1a01:df::7005]/ottrrs.hl.chinamobile.com/PLTV/88888888/224/3221226021/index.m3u8\r\n#EXTINF:-1 tvg-name=\"CCTV4\" tvg-logo=\"https://epg.112114.xyz/logo/cctv4.png\" group-title=\"•央视「IPV6」\",CCTV4「IPV6」\r\nhttp://[2409:8087:1a01:df::7005]/ottrrs.hl.chinamobile.com/PLTV/88888888/224/3221226428/index.m3u8\r\n#EXTINF:-1 tvg-name=\"CCTV5\" tvg-logo=\"https://epg.112114.xyz/logo/cctv5.png\" group-title=\"•央视「IPV6」\",CCTV5「IPV6」\r\n#EXT-X-ENDLIST")]
        [Theory]
        public void TestTVGPlayList(string m3u)
        {
            var result = PlayList.TryParse(m3u, out var list);
            Assert.True(result);
            Assert.NotNull(list);
          
        }


        [InlineData("http://192.168.11.253/index/api/getM3u8?app=live&stream=test&start=1712455980&end=1712456980")]
        [Theory]
        public async void TestNetwokPlayList(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode);
            var content = await response.Content.ReadAsStringAsync();         
            var result = PlayList.TryParse(content, out var list);
            Assert.True(result);
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Version);
        }
    }
}