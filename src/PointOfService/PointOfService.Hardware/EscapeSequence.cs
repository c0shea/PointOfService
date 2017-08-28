namespace PointOfService.Hardware
{
    public static class EscapeSequence
    {
        private const char EscapeCharacter = (char)27;
        public static string Esc => $"{EscapeCharacter}|";

        // Print Line
        public static string Normal => $"{Esc}N";

        public static string Bold(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}bC";
        public static string Underline(bool isEnabled = true, double? thickness = null) => $"{Esc}{(isEnabled ? "" : "!")}{thickness?.ToString() ?? ""}uC";
        public static string Italic(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}iC";

        public static string AlternateColor(double? color = null) => $"{Esc}{color?.ToString() ?? ""}rC";
        public static string ReverseVideo(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}rvC";
        public static string Shading(double? percentShade = null) => $"{Esc}{percentShade?.ToString() ?? ""}sC";

        public static string SingleHighAndWide => $"{Esc}1C";
        public static string DoubleWide => $"{Esc}2C";
        public static string DoubleHigh => $"{Esc}3C";
        public static string DoubleHighAndWide => $"{Esc}4C";

        public static string ScaleHorizontally(byte multiple) => $"{Esc}{multiple}hC";
        public static string ScaleVertically(byte multiple) => $"{Esc}{multiple}vC";

        public static string RgbColor(byte red, byte green, byte blue) => $"{Esc}{red:000}{green:000}{blue:000}fC";
        public static string Subscript(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}tbC";
        public static string Superscript(bool isEnabled = true) => $"{Esc}{(isEnabled ? "" : "!")}tpC";
        public static string Strikethrough(bool isEnabled = true, double? thickness = null) => $"{Esc}{(isEnabled ? "" : "!")}{thickness?.ToString() ?? ""}stC";

        public static string Center => $"{Esc}cA";
        public static string Right => $"{Esc}rA";
        public static string Left => $"{Esc}lA";

        // Commands
        public static string PaperCut(byte? percentCut = null) => $"{Esc}{percentCut?.ToString() ?? ""}P";
        public static string FeedAndPaperCut(byte? percentCut = null) => $"{Esc}{percentCut?.ToString() ?? ""}fP";
        public static string FeedCutAndStamp(byte? percentCut = null) => $"{Esc}{percentCut?.ToString() ?? ""}sP";
        public static string FireStamp => $"{Esc}sL";

        public static string PrintBitmap(short bitmapNumber) => $"{Esc}{bitmapNumber}B";
        public static string PrintTopLogo => $"{Esc}tL";
        public static string PrintBottomLogo => $"{Esc}bL";

        public static string FeedLines(short? lines) => $"{Esc}{lines?.ToString() ?? ""}lF";
        public static string FeedUnits(int? units) => $"{Esc}{units?.ToString() ?? ""}uF";
        public static string FeedReverse(short? lines) => $"{Esc}{lines?.ToString() ?? ""}rF";
    }
}
