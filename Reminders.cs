//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DailyPlanner
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reminders
    {
        public int ReminderId { get; set; }
        public int NoteId { get; set; }
        public System.DateTime ReminderTime { get; set; }
    
        public virtual Notes Notes { get; set; }
    }
}
