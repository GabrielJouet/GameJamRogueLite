public interface IActivable
{
    bool CanBeActivated { get; set; }

    bool CanBeDesactivated { get; set; }

    void Activate();

    void Desactivate();
}