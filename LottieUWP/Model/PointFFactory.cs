﻿using System.Numerics;
using LottieUWP.Model.Animatable;
using LottieUWP.Utils;
using Newtonsoft.Json;

namespace LottieUWP.Model
{
    internal class PointFFactory : IAnimatableValueFactory<Vector2?>
    {
        internal static readonly PointFFactory Instance = new PointFFactory();

        private PointFFactory()
        {
        }

        public Vector2? ValueFromObject(JsonReader reader, float scale)
        {
            JsonToken token = reader.Peek();
            if (token == JsonToken.StartArray)
            {
                return JsonUtils.JsonToPoint(reader, scale);
            }
            if (token == JsonToken.StartObject)
            {
                return JsonUtils.JsonToPoint(reader, scale);
            }
            if (token == JsonToken.Integer || token == JsonToken.Float)
            {
                // This is the case where the static value for a property is an array of numbers. 
                // We begin the array to see if we have an array of keyframes but it's just an array 
                // of static numbers instead. 
                var point = new Vector2(reader.NextDouble() * scale, reader.NextDouble() * scale);
                while (reader.HasNext())
                {
                    reader.SkipValue();
                }
                return point;
            }

            throw new System.ArgumentException("Cannot convert json to point. Next token is " + token);
        }
    }
}