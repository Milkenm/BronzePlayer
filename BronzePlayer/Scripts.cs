#region Using
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

using BronzePlayer; // [Project] Bronze Player

using Microsoft.Win32;

using NAudio.Wave; // [NuGet] NAudio

using NYoutubeDL; // [NuGet] NYouTubeDL
#endregion Using

public class Scripts
{
    #region Refers
    private static readonly string connection_string = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Bronze Player\BD.mdb";
    private static OleDbConnection database_connection = new OleDbConnection(connection_string);

    public static Config config = new Config();
    public static Lang lang = new Lang();

    public static DataBase dataBase = new DataBase();
    public static FileConverters fileConverter = new FileConverters();
    public static Tools tools = new Tools();
    public static Music music = new Music();

    public static Random random = new Random();
    public static SoundPlayer soundPlayer = new SoundPlayer();
    public static MediaElement mediaElement = new MediaElement();
    public static WaveOut jukebox = new WaveOut();
    #endregion Refers

    #region Codes
    readonly string code_pelica = ":01:"; // '
    readonly string code_virgula = ":02:"; // ,
    readonly string code_exclamacao = ":03:"; // !
    readonly string code_arroba = ":04:"; // @
    readonly string code_cardinal = ":05:"; // #
    readonly string code_asterisco = ":06:"; // *
    readonly string code_libra = ":07:"; // £
    readonly string code_euro = ":08:"; // €
    readonly string code_dollar = ":09:"; // $
    readonly string code_paragrafo = ":10:"; // §
    readonly string code_percentagem = ":11:"; // %
    readonly string code_e = ":12:"; // &
    readonly string code_par_bico_e = ":13:"; // {
    readonly string code_par_bico_d = ":14:"; // }
    readonly string code_par_liso_e = ":15:"; // [
    readonly string code_par_liso_d = ":16:"; // ]
    readonly string code_par_curvo_e = ":17:"; // (
    readonly string code_par_curvo_d = ":18:"; // )
    readonly string code_igual = ":19:"; // =
    readonly string code_seta_e = ":20:"; // <
    readonly string code_seta_d = ":21:"; // >
    readonly string code_setas_e = ":22:"; // «
    readonly string code_setas_d = ":23:"; // »
    readonly string code_ponto = ":24:"; // .
    readonly string code_barra_e = ":25:"; // \
    readonly string code_barra_d = ":26:"; // /
    #endregion Codes





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
            }
            #region DE3UG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
            finally
            {
                database_connection.Close();
            }
        }
        #endregion Insert(_query)



        #region Select(_query)
        public static List<string> Select(string _query)
        {
            try
            {
                var values = new List<string>();
                using (var command = new OleDbCommand(_query, database_connection))
                {
                    database_connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            values.Add(reader.GetValue(0).ToString());
                        }
                    }
                }
                return values;
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
            finally
            {
                database_connection.Close();
            }
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
            finally
            {
                database_connection.Close();
            }
        }
        #endregion Update(_query)



        #region Delete(_query)
        public void Delete(string _query)
        {
            try
            {
                database_connection.Open();
                var cmd = new OleDbCommand(_query, database_connection);
                cmd.ExecuteNonQuery();
            }
            #region DE3UG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
            finally
            {
                database_connection.Close();
            }
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
                tools.Exception(exception);
                return false;
            }
            #endregion DE3UG
            finally
            {
                database_connection.Close();
            }
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
                tools.Exception(exception);
                return null;
            }
            #endregion DE3UG
        }
        #endregion SQLIFilter(_string)
    }



    public class FileConverters : Scripts
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
                tools.Exception(exception);
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
                tools.Exception(exception);
                return 0;
            }
            #endregion DE3UG
        }
        #endregion

        #region YouTubeDownloader
        /// https://github.com/BrianAllred/NYoutubeDL/commit/2bd39515eebb8c5fb866687781165804e3b0579f

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
                youtubeDl.YoutubeDlPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Milkenm\Bronze Player", "Path", null).ToString() + "youtube-dl.exe";
                await youtubeDl.PrepareDownloadAsync();
                await youtubeDl.DownloadAsync();
            }
            #region DE3UG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
        }
        #endregion YouTubeDownloader

        #region Exception
        public void Exception(Exception _exception)
        {
            try
            {
                var stackTrace = new StackTrace(_exception, true);
                var frame = stackTrace.GetFrame(0);

                if (config.debug == true)
                {
                    MessageBox.Show(_exception.Message + "\n\n\nMethod: " + frame.GetMethod().Name + "" + "\nLinha: " + frame.GetFileLineNumber(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch { }
        }
        #endregion Exception

        #region Error
        public void Error(string _cause)
        {
            throw new Exception(_cause);
        }
        #endregion Error

        #region ReplaceWithCode
        public enum ReplaceType
        {
            Convert,
            Original,
        }

        public string ReplaceWithCode(string _string, ReplaceType _type)
        {
            try
            {
                if (_type == ReplaceType.Convert)
                {
                    var regex = new Regex("[']");
                    _string = regex.Replace(_string, code_pelica);

                    regex = new Regex("[,]");
                    _string = regex.Replace(_string, code_virgula);

                    regex = new Regex("[!]");
                    _string = regex.Replace(_string, code_exclamacao);

                    regex = new Regex("[@]");
                    _string = regex.Replace(_string, code_arroba);

                    regex = new Regex("[#]");
                    _string = regex.Replace(_string, code_cardinal);

                    regex = new Regex("[*]");
                    _string = regex.Replace(_string, code_asterisco);

                    regex = new Regex("[£]");
                    _string = regex.Replace(_string, code_libra);

                    regex = new Regex("[€]");
                    _string = regex.Replace(_string, code_euro);

                    regex = new Regex("[$]");
                    _string = regex.Replace(_string, code_dollar);

                    regex = new Regex("[§]");
                    _string = regex.Replace(_string, code_paragrafo);

                    regex = new Regex("[%]");
                    _string = regex.Replace(_string, code_percentagem);

                    regex = new Regex("[&]");
                    _string = regex.Replace(_string, code_e);

                    regex = new Regex("[{]");
                    _string = regex.Replace(_string, code_par_bico_e);

                    regex = new Regex("[}]");
                    _string = regex.Replace(_string, code_par_bico_d);

                    regex = new Regex("[[]");
                    _string = regex.Replace(_string, code_par_liso_e);

                    regex = new Regex("[]]");
                    _string = regex.Replace(_string, code_par_liso_d);

                    regex = new Regex("[(]");
                    _string = regex.Replace(_string, code_par_curvo_e);

                    regex = new Regex("[)]");
                    _string = regex.Replace(_string, code_par_curvo_d);

                    regex = new Regex("[=]");
                    _string = regex.Replace(_string, code_igual);

                    regex = new Regex("[<]");
                    _string = regex.Replace(_string, code_seta_e);

                    regex = new Regex("[>]");
                    _string = regex.Replace(_string, code_seta_d);

                    regex = new Regex("[«]");
                    _string = regex.Replace(_string, code_setas_e);

                    regex = new Regex("[»]");
                    _string = regex.Replace(_string, code_setas_d);

                    regex = new Regex("[.]");
                    _string = regex.Replace(_string, code_ponto);

                    regex = new Regex(@"[\\]");
                    _string = regex.Replace(_string, code_barra_e);

                    regex = new Regex("[/]");
                    _string = regex.Replace(_string, code_barra_d);



                    return _string;
                }
                else
                {
                    var regex = new Regex(code_pelica);
                    _string = regex.Replace(_string, "'");

                    regex = new Regex(code_virgula);
                    _string = regex.Replace(_string, ",");

                    regex = new Regex(code_exclamacao);
                    _string = regex.Replace(_string, "!");

                    regex = new Regex(code_arroba);
                    _string = regex.Replace(_string, "@");

                    regex = new Regex(code_cardinal);
                    _string = regex.Replace(_string, "#");

                    regex = new Regex(code_asterisco);
                    _string = regex.Replace(_string, code_asterisco);

                    regex = new Regex(code_libra);
                    _string = regex.Replace(_string, "£");

                    regex = new Regex(code_euro);
                    _string = regex.Replace(_string, "€");

                    regex = new Regex(code_dollar);
                    _string = regex.Replace(_string, "$");

                    regex = new Regex(code_paragrafo);
                    _string = regex.Replace(_string, "§");

                    regex = new Regex(code_percentagem);
                    _string = regex.Replace(_string, "%");

                    regex = new Regex(code_e);
                    _string = regex.Replace(_string, "&");

                    regex = new Regex(code_par_bico_e);
                    _string = regex.Replace(_string, "{");

                    regex = new Regex(code_par_bico_d);
                    _string = regex.Replace(_string, "}");

                    regex = new Regex(code_par_liso_e);
                    _string = regex.Replace(_string, "[");

                    regex = new Regex(code_par_liso_d);
                    _string = regex.Replace(_string, "]");

                    regex = new Regex(code_par_curvo_e);
                    _string = regex.Replace(_string, "(");

                    regex = new Regex(code_par_curvo_d);
                    _string = regex.Replace(_string, ")");

                    regex = new Regex(code_igual);
                    _string = regex.Replace(_string, "=");

                    regex = new Regex(code_seta_e);
                    _string = regex.Replace(_string, "<");

                    regex = new Regex(code_seta_d);
                    _string = regex.Replace(_string, ">");

                    regex = new Regex(code_setas_e);
                    _string = regex.Replace(_string, "«");

                    regex = new Regex(code_setas_d);
                    _string = regex.Replace(_string, "»");

                    regex = new Regex(code_ponto);
                    _string = regex.Replace(_string, ".");

                    regex = new Regex(code_barra_e);
                    _string = regex.Replace(_string, @"\");

                    regex = new Regex(code_barra_d);
                    _string = regex.Replace(_string, "/");



                    return _string;
                }
            }
            catch (Exception exception)
            {
                tools.Exception(exception);
                return null;
            }
        }
        #endregion ReplaceWithCode
    }



    public class Music : Scripts
    {
        #region State / Vars
        public AudioFileReader audioFile;
        long position;

        public State state = State.Null;
        public enum State
        {
            Playing,
            Paused,
            Stopped,
            Null,
        }
        #endregion State / Vars



        #region Play
        public void Play(string _file)
        {
            try
            {
                if (state == State.Paused)
                {
                    if (audioFile.FileName != _file)
                    {
                        audioFile = new AudioFileReader(_file);
                        if (state == State.Playing || state == State.Paused)
                        {
                            Stop();
                        }
                        jukebox.Init(audioFile);
                        jukebox.Play();
                        state = State.Playing;
                    }
                    else
                    {
                        audioFile.Position = position;
                        jukebox.Play();
                        state = State.Playing;
                    }
                }
                else
                {
                    if (_file != "" && _file != null)
                    {
                        audioFile = new AudioFileReader(_file);
                        if (state == State.Playing || state == State.Paused)
                        {
                            Stop();
                        }
                        jukebox.Init(audioFile);
                        jukebox.Play();
                        state = State.Playing;
                    }
                    else
                    {
                        if (audioFile.FileName != "" && audioFile.FileName != null)
                        {
                            if (state == State.Playing || state == State.Paused)
                            {
                                jukebox.Stop();
                            }
                            audioFile.Position = 0;
                            jukebox.Init(audioFile);
                            jukebox.Play();
                            state = State.Playing;
                        }
                    }
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
        }
        #endregion Play

        #region Pause
        public void Pause()
        {
            try
            {
                if (state == State.Playing)
                {
                    position = audioFile.Position;
                    jukebox.Stop();
                    state = State.Paused;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
        }
        #endregion Pause

        #region Stop
        public void Stop()
        {
            try
            {
                if (state == State.Playing || state == State.Paused)
                {
                    if (state == State.Playing)
                    {
                        jukebox.Stop();
                    }
                    state = State.Stopped;
                }
            }
            #region DEBUG
            catch (Exception exception)
            {
                tools.Exception(exception);
            }
            #endregion DE3UG
        }
        #endregion Stop
    }
}



/*
 *  ╔═══════════════════════════════════════════════════════════════════════════════════════════╗
 *  ║▓▒░           # THE MAP License | 1.0 | Copyright © 2019 Milkenm                        ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░   This file has been stolen* from https://github.com/Milkenm/BronzePlayer           ░▒▓║
 *       ║▓▒░  If you received a copy of this file, and can see this message, congrats,           ░▒▓║
 *        ║▓▒░     the person that gave you this file is a nice human!                             ░▒▓║
 *        ║▓▒░ Everyone is allowed to copy and distribute verbatim copies of this license document,░▒▓║
 *        ║▓▒░     but changing it is definitly not allowed.                                       ░▒▓║
 *       ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *   ║▓▒░ > TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION                   ░▒▓║
 *  ║▓▒░                                                                                     ░▒▓║
 *  ║▓▒░  1. The stolen file can be used, because it's a file, and you can edit/share it,    ░▒▓║
 *  ║▓▒░         as long as you keep this license file.                                      ░▒▓║
 *  ║▓▒░  2. Don't delete this license (I made it look like a map so you won't delete it).   ░▒▓║
 *   ║▓▒░ 3. No, you cannot reshape the map.                                                  ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░    *jk, this file was not stolen, chill. - or was it?                               ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                             Typed by: Milkenm       ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *   ╚═══════════════════════════════════════════════════════════════════════════════════════════╝
 */
