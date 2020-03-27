using System.Collections.Generic;

public interface ILockable
{
    List<Lock> Locks { get; set; }

    void Lock();

    void OpenOneLock(Lock other);

    void Open();
}