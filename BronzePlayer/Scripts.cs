#region Using
using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

using BronzePlayer; // [Project] Bronze Player

using MediaToolkit; // [NuGet] MediaToolKit
using MediaToolkit.Model; // [NuGet] MediaToolKit

using Microsoft.Win32;

using NAudio.Wave; // [NuGet] NAudio

using NYoutubeDL; // [NuGet] NYouTubeDL
#endregion Using

public class Scripts
{
    #region Vars
    string regPath = @"Software\Milkenm\Bronze Player";
    #endregion Vars

    #region Refers
    #region Database Connection
    private static readonly string connection_string = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD.mdb";
    private static OleDbConnection database_connection = new OleDbConnection(connection_string);
    #endregion Database Connection


    #region Project Items
    public static Config config = new Config();
    public static Lang lang = new Lang();
    #endregion Project Items

    public static DataBase dataBase = new DataBase();
    public static FileConverter fileConverter = new FileConverter();
    public static Tools tools = new Tools();

    public static Random random = new Random();
    public static SoundPlayer soundPlayer = new SoundPlayer();
    public static MediaElement mediaElement = new MediaElement();
    public static WaveOut waveOut = new WaveOut();
    #endregion Refers





    public class DataBase : Scripts
    {
        #region Commands
        #region Insert(_query)
        public static void Insert(string _query)
        {
            try
            {
                database_connection.Open();
                var cmd = new OleDbCommand(_query, database_connection);
                cmd.ExecuteNonQuery();
                database_connection.Close();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.Insert()");
                }
            }
            #endregion DE3UG
        }
        #endregion Insert(_query)



        #region Select(_query)
        public static string Select(string _query)
        {
            try
            {
                database_connection.Close();
                database_connection.Open();

                OleDbCommand cmd = new OleDbCommand(_query, database_connection);

                OleDbDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string valor = dr.GetValue(0).ToString();

                dr.Close();
                database_connection.Close();

                return valor;
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.Select()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
            #endregion DE3UG
        }
        #endregion Select(_query)



        #region Update(_query)
        public void Update(string _query)
        {
            try
            {
                database_connection.Open();

                var cmd = new OleDbCommand(_query, database_connection);
                cmd.ExecuteNonQuery();

                database_connection.Close();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.Update()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion DE3UG
        }
        #endregion Update(_query)



        #region Delete(_query)
        public void Delete(string _query)
        {
            try
            {
                database_connection.Open();

                var cmd = new OleDbCommand(_query, database_connection);

                database_connection.Close();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.Delete()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion DE3UG
        }
        #endregion Delete(_query)
        #endregion Commands
        
        #region Login(_user, _password)
        public bool Login(string _user, string _password)
        {
            try
            {
                database_connection.Open();

                string sql = "SELECT COUNT(*) FROM Login_Funcionario WHERE Login = '" + SQLIFilter(_user) + "' AND Password = '" + SQLIFilter(_password) + "'";

                var cmd = new OleDbCommand(sql, database_connection);

                int codigo_ID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                database_connection.Close();

                if (codigo_ID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.Login()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            #endregion DE3UG
        }
        #endregion Login(_user, _password)
        
        #region SQLIFilter(_string)
        public string SQLIFilter(string _string)
        {
            try
            {
                _string = _string.Replace("'", "");
                _string = _string.Replace(";", "");

                return _string;
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.DataBase.SQLIFilter()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
            #endregion DE3UG
        }
        #endregion SQLIFilter(_string)
    }



    public class FileConverter : Scripts
    {
        #region Mp3ToWav(_indir, _outdir)
        public void Mp3ToWav(string _indir, string _outdir)
        {
            try
            {
                var mp3 = new Mp3FileReader(_indir);
                var wavestream = WaveFormatConversionStream.CreatePcmStream(mp3);
                WaveFileWriter.CreateWaveFile(_outdir, wavestream);
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.FileConverter.Mp3ToWav()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion DE3UG
        }
        #endregion Mp3ToWav(_indir, _outdir)
    }



    public class Tools : Scripts
    {
        #region Random(_min, _max)
        public int Random(int _min, int _max)
        {
            try
            {
                return random.Next(_min, _max);
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Scripts.Tools.Random()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return 0;
            }
            #endregion DE3UG
        }
        #endregion
        
        #region YouTubeDownloader
        /// https://github.com/BrianAllred/NYoutubeDL/commit/2bd39515eebb8c5fb866687781165804e3b0579f ///

        public async Task YouTubeDownloader(string _link, string _extension, string _path)
        {
            try
            {
                var youtubeDl = new YoutubeDL();

                #region File Name
                string name = Random(100000, 999999).ToString();
                while (File.Exists(_path + @"\" + name + _extension))
                {
                    name = Random(100000, 999999).ToString();
                }
                #endregion File Name

                youtubeDl.Options.FilesystemOptions.Output = _path + @"\" + name + _extension;
                if (_extension == ".mp3")
                {
                    youtubeDl.Options.PostProcessingOptions.ExtractAudio = true;
                }
                youtubeDl.VideoUrl = _link;
                youtubeDl.YoutubeDlPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Milkenm\Bronze Player", "Path", null).ToString() + "youtube-dl.exe";
                await youtubeDl.PrepareDownloadAsync();
                await youtubeDl.DownloadAsync();
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    tools.Exception(exception);
                }
            }
            #endregion DE3UG
        }
        #endregion YouTubeDownloader
        
        #region ListBAddItem
        public class ListBAddItem
        {
            public string name;
            public string value;

            public override string ToString()
            {
                return this.name;
            }
        }
        #endregion ListBAddItem
        
        #region Exception
        public void Exception(Exception _exception)
        {
            var stackTrace = new StackTrace(_exception, true);
            var frame = stackTrace.GetFrame(0);

            MessageBox.Show(_exception.Message + "\n\n\nMethod: " + frame.GetMethod().Name + "" +
                "\nLinha: " + frame.GetFileLineNumber(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Exception
        
        #region FakeError
        public void FakeError(string _cause)
        {
            throw new Exception(_cause);
        }
        #endregion FakeError
    }
}
