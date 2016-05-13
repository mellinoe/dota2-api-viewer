using System;
using Newtonsoft.Json;

namespace DotaApiViewer
{
    [JsonConverter(typeof(PlayerSlotConverter))]
    public struct PlayerSlot
    {
        private readonly byte _data;

        public PlayerSlot(byte data)
        {
            _data = data;
        }

        public bool IsRadiant
        {
            get
            {
                return (_data & (1 << 7)) == 0;
            }
        }

        public int Slot
        {
            get
            {
                return _data & 7;
            }
        }

        public override string ToString()
        {
            return $"{(IsRadiant ? "Radiant" : "Dire")} {Slot}";
        }

        public static implicit operator PlayerSlot(byte b) => new PlayerSlot(b);
        public static implicit operator byte(PlayerSlot b) => b._data;

        public class PlayerSlotConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PlayerSlot);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                int value = reader.ReadAsInt32().Value;
                return new PlayerSlot((byte)value);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((PlayerSlot)value)._data);
            }
        }
    }
}
