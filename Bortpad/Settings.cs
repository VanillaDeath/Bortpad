using Bortpad.Properties;
using SharpConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WilsonUtils;

namespace Bortpad
{
    public class Settings
    {
        #region Properties

        public Configuration Config
        {
            get; internal set;
        } = new();

        public string ConfigFile
        {
            get; internal set;
        } = Defaults.Default.DefaultConfigFile;

        public static string ProgramName
        {
            get;
        } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        #endregion Properties

        #region Constructors

        public Settings(string configFile = "{0}.cfg")
        {
            ConfigFile = string.Format(configFile ?? Defaults.Default.DefaultConfigFile, ProgramName);
            _ = Load();
        }

        #endregion Constructors

        #region Instance Methods

        public T Get<T>(string key, string section = null)
        {
            try
            {
                /*
                if (!LoadedFromFile && !Load())
                {
                    throw new InvalidDataException(Resources.CantLoadSettings);
                }

                if (!Config.Contains(section ??= Defaults.Default.DefaultSection) && Config.Add(section).Name != section)
                {
                    throw new KeyNotFoundException(string.Format(Resources.SettingsSectionNotFound, section));
                }
                */
                if (!Config.Contains(section ??= Defaults.Default.DefaultSection) || !Config[section].Contains(key ??= ""))
                {
                    if (Defaults.Default[$"{section}_{key}"] is not null)
                    {
                        Set(key, Defaults.Default[$"{section}_{key}"], section);
                    }
                    if (!Config.Contains(section) || !Config[section].Contains(key))
                    {
                        throw new KeyNotFoundException(string.Format(Resources.SettingsKeyNotFound, section, key));
                    }
                }
                return Config[section][key].GetValue<T>();
            }
            catch (Exception e)
            {
                DialogResult resetConfig = MsgBox.Show(
                    e.Message,
                    Resources.ResetConfigFile,
                    ProgramName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2
                    );
                if (resetConfig == DialogResult.Yes)
                {
                    // File.Move(ConfigFile, $"{ConfigFile}.bak");
                    File.Delete(ConfigFile);
                    return Get<T>(key, section);
                    /*
                    if (!Load(saveDefaultsIfNoFile: true))
                    {
                        _ = MsgBox.Show(
                            Resources.SettingsResetFailed,
                            Resources.SettingsResetFailedDesc,
                            ProgramName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
                        return default;
                    }
                    */
                }
                return default;
            }
        }

        internal static Configuration Load(string configFile)
        {
            return File.Exists(configFile) ? Configuration.LoadFromFile(configFile) : default;
        }

        internal bool Load()
        {
            if (!File.Exists(ConfigFile))
            {
                return false;
            }
            Config = Load(ConfigFile);
            return true;
        }

        internal void Save()
        {
            Config.SaveToFile(ConfigFile);
        }

        internal void Set<T>(string key, T value, string section = null, bool save = true)
        {
            try
            {
                if (key is null || key.Trim().Length == 0)
                {
                    throw new ArgumentException(Resources.InvalidSettingName);
                }
                Config[section ?? Defaults.Default.DefaultSection][key].SetValue(value);
                if (save)
                {
                    Save();
                }
            }
            catch (Exception e)
            {
                _ = MsgBox.Show(
                    e.Message,
                    ProgramName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        internal bool Toggle(string key, string section = null, bool save = true)
        {
            section ??= Defaults.Default.DefaultSection;
            Set(key, !Get<bool>(key, section), section, save);
            return Get<bool>(key, section);
        }

        #endregion Instance Methods
    }
}