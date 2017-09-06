using System;
using System.Collections.Generic;
using TelegramBot.Business.Domain.Core;

namespace TelegramBot.Business.Domain.Entities
{
    public class Item : IEntity{
        public Item(long id ,string name,bool hasData =false,bool hasHyperLink =false,long itemId=1,string question ="انتخاب کنید"){
            Id = id;
            Text = name;
            HasData = hasData;
            ItemId = itemId;
            Question = question;
            Items = new List<Item>();
            CreatedTime = DateTime.Now;
        }
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Text { get; set; }
        public string Question { get; set; }
        public bool HasData { get; set; }
        public List<Item> Items { get; set; }
        public long ItemId { get; set; }
        public List<ItemDescription> ItemDescriptions { get; set; }
    }
}