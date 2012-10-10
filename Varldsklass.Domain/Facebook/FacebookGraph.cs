//-----------------------------------------------------------------------
// <copyright file="FacebookGraph.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace DotNetOpenAuth.ApplicationBlock.Facebook
    {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Varldsklass.Domain.Entities;

    [DataContract]
    public class FacebookGraph
        {
        private static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookGraph));

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        public static FacebookGraph Deserialize(string json)
            {
            if (String.IsNullOrEmpty(json))
                {
                throw new ArgumentNullException("json");
                }

            return Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            }

        public static FacebookGraph Deserialize(Stream jsonStream)
            {
            if (jsonStream == null)
                {
                throw new ArgumentNullException("jsonStream");
                }

            return (FacebookGraph)jsonSerializer.ReadObject(jsonStream);
            }
        [DataContract]
        public class FacebookPageFeed
            {

            private static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookPageFeed));

            public static FacebookPageFeed Deserialize(string json)
                {
                if (string.IsNullOrEmpty(json))
                    {
                    throw new ArgumentNullException("json");
                    }

                return Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(json)));
                }

            public static FacebookPageFeed Deserialize(Stream jsonStream)
                {
                if (jsonStream == null)
                    {
                    throw new ArgumentNullException("jsonStream");
                    }

                return (FacebookPageFeed)jsonSerializer.ReadObject(jsonStream);
                }

            [DataMember(Name = "id")]
            public string ID { get; set; }

            [DataMember(Name = "feed")]
            public FacebookFeedGraph PageFeed { get; set; }
            }

        [DataContract]
        public class FeedPost
            {
            [DataMember(Name = "from")]
            public NameIDPair From { get; set; }

            [DataMember(Name = "created_time")]
            public string CreatedTime { get; set; }

            [DataMember(Name = "message")]
            public string Message { get; set; }

            [DataMember(Name = "picture")]
            public string PictureURL { get; set; }

            }

        [DataContract]
        public class NameIDPair
            {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "id")]
            public string ID { get; set; }
            }
        [DataContract]
        public class FacebookFeedGraph
            {

            [DataMember(Name = "data")]
            public List<FeedPost> Posts { get; set; }



            }
        }
    }
