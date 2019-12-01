using Renci.SshNet;
using Renci.SshNet.Async;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Server
{
public  class Sftp
  {


    public SftpClient sftpClient { get; set; }


    public Sftp(string host, string user, string pass)
    {
      if (host.Length < 4 && user.Length < 1 && pass.Length < 1)
        throw new Exception("");

      sftpClient = new SftpClient(host, user, pass);
    }
    public async Task SftpUploadAsync(string localFile, string remoteFile)
    {

      sftpClient.Connect();

      using (var localStream = File.OpenRead(localFile))
      {
        await sftpClient.UploadAsync(localStream, remoteFile);
      }
      sftpClient.Disconnect();
    }
    public async Task SftpDownloadAsync(string localDir, string remoteFile)
    {

      sftpClient.Connect();

      if (!Directory.Exists(localDir))
        Directory.CreateDirectory(localDir);
      var localFile = Path.Combine(localDir, System.IO.Path.GetFileName(remoteFile));
      using (var localStream = new FileStream(localFile, FileMode.OpenOrCreate, FileAccess.Write))
      {
        await sftpClient.DownloadAsync(remoteFile, localStream);
      }
      sftpClient.Disconnect();
    }


  }
}
