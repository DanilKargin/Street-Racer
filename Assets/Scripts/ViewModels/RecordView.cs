using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
	public class RecordView
	{
		public int Id { get; set; }
		public DateTime RecordDate { get; set; }
		public string RideType { get; set; } = string.Empty;
		public int Score { get; set; }
		public string PlayerName { get; set; }
	}
}
