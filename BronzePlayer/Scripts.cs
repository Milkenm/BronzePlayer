using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Media;
using System.IO;
using System.Threading;
// => NuGet
using NAudio.Wave; // NAudio
using MediaToolkit; // MediaToolKit
using MediaToolkit.Model; // MediaToolKit
// => Projects
using BronzePlayer;
using NYoutubeDL;
using System.Net;
using System.Threading.Tasks;

public class Scripts
{
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
                OleDbCommand cmd = new OleDbCommand(_query, database_connection);
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

                var cmd = new OleDbCommand
                {
                    Connection = database_connection,
                    CommandText = _query
                };
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

                var cmd = new OleDbCommand
                {
                    Connection = database_connection,
                    CommandText = _query
                };
                cmd.ExecuteNonQuery();

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

                _user = SQLIFilter(_user);
                _password = SQLIFilter(_password);

                string sql = "SELECT COUNT(*) FROM Login_Funcionario WHERE Login = '" + _user + "' AND Password = '" + _password + "'";

                var cmd = new OleDbCommand
                {
                    Connection = database_connection,
                    CommandText = sql
                };

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
                using (Mp3FileReader mp3 = new Mp3FileReader(_indir))
                {
                    using (WaveStream wavestream = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(_outdir, wavestream);
                    }
                }
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
        public async Task YouTubeDownloader(string _link, string _extension, string _path)
        {
            try
            {
                ///
                // https://github.com/BrianAllred/NYoutubeDL/commit/2bd39515eebb8c5fb866687781165804e3b0579f
                ///

                string name = "";
                bool loop = true;

                while (loop != false)
                {
                    name = Convert.ToString(tools.Random(100000, 999999));
                    if (!File.Exists(_path + @"\" + name + _extension))
                    {
                        loop = false;
                    }
                }

                var youtubeDl = new YoutubeDL();

                youtubeDl.Options.FilesystemOptions.Output = _path + @"\" + name + _extension;
                if (_extension == ".mp3")
                {
                    youtubeDl.Options.PostProcessingOptions.ExtractAudio = true;
                    
                }
                youtubeDl.VideoUrl = _link;
                await youtubeDl.PrepareDownloadAsync();
                await youtubeDl.DownloadAsync();

                while (!File.Exists(_path + @"\" + name + _extension)) { }
            }
            #region DE3UG
            catch (Exception exception)
            {
                if (config.debug == true)
                {
                    MessageBox.Show(exception.Message.ToString(), "DE3UG - Scripts.Tools.YouTubeDownloader()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
