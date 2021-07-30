using System.IO;

namespace Game2048
{
    public enum ShortcutLocation
    {
        DESKTOP,
        COMMON_DESKTOP,
        START_MENU
    }
    public class ShortcutManager
    {
        public static void CreateShortcut(string linkName, string apppath, string iconpath, ShortcutLocation location, string hotkey)
        {
            object shortPath = null;
            // Определяем место, где будет создан ярлык
            switch (location)
            {
                case ShortcutLocation.DESKTOP:
                    {
                        shortPath = "Desktop";
                    }; break;
                case ShortcutLocation.START_MENU:
                    {
                        shortPath = "StartMenu";
                    }; break;
                case ShortcutLocation.COMMON_DESKTOP:
                    {
                        shortPath = "AllUsersDesktop";
                    }; break;
                default: shortPath = "Desktop"; break;
            }

            WshShell shell = new WshShell();
            // Получаем полный адрес ярлыка
            string link = Path.Combine((string)shell.SpecialFolders.Item(ref shortPath), linkName + @".lnk");
            // Создаем объект ярлыка
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(link);
            if (!hotkey.Equals(string.Empty))
            {
                // Назначаем горячую клавишу
                shortcut.Hotkey = hotkey;
            }
            // Описание ярлыка
            shortcut.Description = linkName;

            shortcut.IconLocation = iconpath;
            //Указываем рабочую папку
            shortcut.WorkingDirectory = Path.GetDirectoryName(apppath);
            // Указываем путь для программы
            shortcut.TargetPath = apppath;
            // Вызываем метод Save(), который и создаст ярлык         
            shortcut.Save();
        }
    }
}
