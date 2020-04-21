﻿using System;

namespace Todo.WebAPI.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsTypeOf<TType>(this Type type)
        {
            return type == typeof(TType);
        }
    }
}