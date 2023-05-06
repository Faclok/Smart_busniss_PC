
namespace Assets.MultiSetting
{

    /// <summary>
    /// Перечесление используемое для отлова ошибок и Debugger
    /// </summary>
    public enum TypeException
    {
        /// <summary>
        /// Проблеммы с сервером или его подключением.
        /// </summary>
        DisconnectedServer,
        
        /// <summary>
        /// Проблемы с интернетом или его подключением.
        /// </summary>
        NotNetwork,
        
        /// <summary>
        /// Системная ошибка
        /// </summary>
        SystemFailed,
        
        /// <summary>
        /// Логическая ошибка, НЕ УЧИТЫВАЕТСЯ ПРИ DEBUGGER
        /// </summary>
        LogicApplication
    }
}
