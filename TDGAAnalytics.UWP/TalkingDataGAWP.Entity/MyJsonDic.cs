using System;
using System.Collections.Generic;
using System.Text;

namespace TalkingDataGAWP.Entity
{
	internal class MyJsonDic : Dictionary<string, object>
	{
		private string stringPrefix = "\"";

		private void addString(string s, StringBuilder sb)
		{
			sb.Append(this.stringPrefix).Append(s).Append(this.stringPrefix);
		}

		public string toJsonString()
		{
			if (base.get_Keys().get_Count() > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("{");
				using (Dictionary<string, object>.KeyCollection.Enumerator enumerator = base.get_Keys().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.get_Current();
						this.addString(current, stringBuilder);
						stringBuilder.Append(":");
						if (base.get_Item(current).GetType().Equals(typeof(string)))
						{
							this.addString(base.get_Item(current).ToString(), stringBuilder);
						}
						else
						{
							stringBuilder.Append(base.get_Item(current));
						}
						stringBuilder.Append(",");
					}
				}
				StringBuilder expr_B3 = stringBuilder;
				expr_B3.Remove(expr_B3.Length - 1, 1).Append("}");
				return stringBuilder.ToString();
			}
			return "{}";
		}
	}
}
