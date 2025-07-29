namespace SmartWorkMode
{
    public static class Debug
    {
        public static void Log(object message)
        {
#if DEBUG
            Verse.Log.Message($"[{SmartWorkModeMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
