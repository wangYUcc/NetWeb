using Renci.SshNet;
using Renci.SshNet.Async;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SSH
{
  public class SSH
  {

    public SshClient sshClient { get; set; }



    public SSH(string host, string user, string pass)
    {
      if (host.Length < 4 && user.Length < 1 && pass.Length < 1)
        throw new Exception("");
      sshClient = new SshClient(host, user, pass);
    }

    public SSH(string host, string user, PrivateKeyFile[] key)
    {
      if (host.Length < 4 && user.Length < 1)
        throw new Exception("");
      sshClient = new SshClient(host, user,  key);
    }



    public string SshCommand(SshClient sshClient, string cmd)
    {
      using (sshClient)
      {
        sshClient.Connect();
        var output = sshClient.RunCommand(cmd);
        return output.ToString();
      }
    }
    public bool Disconnect()
    {
      using (sshClient)
      {
        sshClient.Disconnect();
        return true;
      }
    }
  }
}
