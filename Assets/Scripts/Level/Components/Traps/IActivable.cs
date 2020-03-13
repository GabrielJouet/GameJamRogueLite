using System.Collections;

public interface IActivable
{
    bool CanBeActivated { get; set; }

    bool CanBeDesactivated { get; set; }

    bool IsActive { get; set; }


    void Activate();

    void Desactivate();

    IEnumerator ResetActiveState();
}