using System.IO;

namespace UpdateDatabase
{
    class PathsEngine
    {
        public static string ConvertRelativePath2RootedPathIfNeeded(
            string path)
        {
            if (Path.IsPathRooted(path) == false)
            {
                return Path.Combine(Configuration.Default.Path, path);
            }
            return path;
        }

        /// <summary>
        /// Corrects backup path according to the configuration file settings.
        /// E.g. if DatabaseBackupsDirectory isnot empty then utility
        /// should open/save all backups the the directory set in the DatabaseBackupsDirectory.
        /// Otherwise use 'default' backup paths.
        /// </summary>
        /// <param name="defaultBackupPath"></param>
        /// <returns></returns>
        public static string CorrectBackupPathAccordingToConfig(
            string defaultBackupPath)
        {
            if (!string.IsNullOrEmpty(Configuration.Default.DatabaseBackupsDirectory.Trim()))
            {
                string backupFileName = Path.GetFileName(defaultBackupPath);

                return Path.Combine(
                    Configuration.Default.DatabaseBackupsDirectory.Trim(),
                    backupFileName);
            }

            //
            // DatabaseBackupsDirectory is not set, so, we dont need change
            // default path.
            //
            return defaultBackupPath;
        }
    }
}
