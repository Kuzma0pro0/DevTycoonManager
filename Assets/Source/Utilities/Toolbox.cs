using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DevIdle
{
    public static class Toolbox
    {
        private static System.Random random = new System.Random();

        public static Sprite GetAvatar(int id)
        {
            return Resources.Load<Sprite>($"Sprites/Face/Man{id + 1}");
        }

        public static string Localize(this string str)
        {
            return Language.Instance.Get(str);
        }

        public static string Localize(this string str, object arg0)
        {
            return Language.Instance.Get(str, arg0);
        }

        public static string Localize(this string str, object arg0, object arg1, object arg2)
        {
            return Language.Instance.Get(str, arg0, arg1, arg2);
        }

        public static void DestroyChildrens(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static T Random<T>(this List<T> list)
        {
            var index = random.Next(0, list.Count);
            return list[index];
        }

        public static T Random<T>(this T[] array)
        {
            var index = random.Next(0, array.Length);
            return array[index];
        }

        public static IEnumerable<T> EnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).OfType<T>();
        }

        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y)
            );
        }

        public static double Clamp(double value, double min, double max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float Choose(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = UnityEngine.Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

        public static double Max(params double[] values)
        {
            var max = double.MinValue;

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }

            return max;
        }
    }
}
