
public class DirectoryManager
{
    public Directory CurrentDirectory;

    public void UndoDirectory()
    {
        if (CurrentDirectory == null)
            return;

        if (CurrentDirectory.PreviousDirectory != null)
        {
            CurrentDirectory.DirectoryContent.SetActive(false);

            CurrentDirectory = CurrentDirectory.PreviousDirectory;
        }
        else
        {
            if (PauseManager.Pause == false)
                return;

            CurrentDirectory = null;

            PausePanel.Instance.SwitchPausePanel(false);
            PauseManager.Instance.ChangePauseManagerStates(true, false);
        }
    }

    /// <summary>
    /// Chage Directory And Save Current Directory As Previous Directory On New Directory
    /// </summary>
    /// <param name="directory"></param>
    public void ChangeDirectory(Directory directory)
    {
        directory.PreviousDirectory = CurrentDirectory;
        CurrentDirectory = directory;
    }
}
