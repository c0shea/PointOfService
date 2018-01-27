using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PointOfService.Hardware.Receipt.Converter
{
    public class CommandConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;
        public override bool CanConvert(Type objectType) => typeof(ICommand).IsAssignableFrom(objectType);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            ICommand command;

            switch (obj["Name"].Value<string>())
            {
                case nameof(Barcode):
                    command = new Barcode();
                    break;

                case nameof(Bitmap):
                    command = new Bitmap();
                    break;

                case nameof(FeedAndPaperCut):
                    command = new FeedAndPaperCut();
                    break;

                case nameof(FeedCutAndStamp):
                    command = new FeedCutAndStamp();
                    break;

                case nameof(FeedLines):
                    command = new FeedLines();
                    break;

                case nameof(FeedReverse):
                    command = new FeedReverse();
                    break;

                case nameof(FeedUnits):
                    command = new FeedUnits();
                    break;

                case nameof(FireStamp):
                    command = new FireStamp();
                    break;

                case nameof(Line):
                    command = new Line();
                    break;

                case nameof(PaperCut):
                    command = new PaperCut();
                    break;

                case nameof(PrintBitmap):
                    command = new PrintBitmap();
                    break;

                case nameof(PrintBottomLogo):
                    command = new PrintBottomLogo();
                    break;

                case nameof(PrintTopLogo):
                    command = new PrintTopLogo();
                    break;

                default:
                    throw new NotImplementedException();
            }

            serializer.Populate(obj.CreateReader(), command);
            return command;
        }
    }
}
