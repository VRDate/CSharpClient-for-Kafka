﻿/**
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Kafka.Client.Utils
{
    using Kafka.Client.Serialization;
    using System;
    using System.Text;

    /// <summary>
    /// Utilty class for managing bits and bytes.
    /// </summary>
    public class BitWorks
    {
        /// <summary>
        /// Converts the value to bytes and reverses them.
        /// </summary>
        /// <param name="value">The value to convert to bytes.</param>
        /// <returns>Bytes representing the value.</returns>
        public static byte[] GetBytesReversed(short value)
        {
            return ReverseBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts the value to bytes and reverses them.
        /// </summary>
        /// <param name="value">The value to convert to bytes.</param>
        /// <returns>Bytes representing the value.</returns>
        public static byte[] GetBytesReversed(int value)
        {
            return ReverseBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Converts the value to bytes and reverses them.
        /// </summary>
        /// <param name="value">The value to convert to bytes.</param>
        /// <returns>Bytes representing the value.</returns>
        public static byte[] GetBytesReversed(long value)
        {
            return ReverseBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Reverse the position of an array of bytes.
        /// </summary>
        /// <param name="inArray">
        /// The array to reverse.  If null or zero-length then the returned array will be null.
        /// </param>
        /// <returns>The reversed array.</returns>
        public static byte[] ReverseBytes(byte[] inArray)
        {
            if (inArray != null && inArray.Length > 0)
            {
                int highCtr = inArray.Length - 1;
                byte temp;

                for (int ctr = 0; ctr < inArray.Length / 2; ctr++)
                {
                    temp = inArray[ctr];
                    inArray[ctr] = inArray[highCtr];
                    inArray[highCtr] = temp;
                    highCtr -= 1;
                }
            }

            return inArray;
        }

        /// <summary>
        /// Return size of a size prefixed string where the size is stored as a 2 byte short
        /// </summary>
        /// <param name="text">The string to write</param>
        /// <param name="encoding">The encoding in which to write the string</param>
        /// <returns></returns>
        public static short GetShortStringLength(string text, string encoding)
        {
            if (string.IsNullOrEmpty(text))
            {
                return (short)2;
            }
            else
            {
                Encoding encoder = Encoding.GetEncoding(encoding);
                var result = (short)2 + (short)encoder.GetByteCount(text);
                return (short)result;
            }
        }

        public static string ReadShortString(KafkaBinaryReader reader, string encoding)
        {
            var size = reader.ReadInt16();
            if (size < 0)
            {
                return null;
            }
            var bytes = reader.ReadBytes(size);
            Encoding encoder = Encoding.GetEncoding(encoding);
            return encoder.GetString(bytes);
        }
    }
}
