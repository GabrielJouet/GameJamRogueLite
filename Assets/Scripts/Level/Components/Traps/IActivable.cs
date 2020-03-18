using System.Collections;

public interface IActivable
{
    bool IsAlwaysActive { get; set; }

    bool IsActive { get; set; }


    void Activate();

    void Desactivate();

    IEnumerator ResetActiveState();
}