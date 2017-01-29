using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_games_input_parser_library_v2
{
    public interface ITextGameAction
    {
        string TakeItems(List<string> items);
        string DropItems(List<string> items);
        string ExamineItems(List<string> items);
        string OperateItems(string keyCommandWhichTriggeredOperate, List<string> items);
        string UseItems(List<string> items);
        string GoTo(List<string> potentialDirectionsOrLocations);
        string LookAround();
    }
}
