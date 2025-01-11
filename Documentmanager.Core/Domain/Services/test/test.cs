// using AutoMapper;
// using Documentmanager.Core.Domain.Dtos.Organizations;
// using Documentmanager.Core.Domain.Dtos.Users;
// using Documentmanager.Core.Domain.Models.Common;
// using Documentmanager.Core.Domain.Models.Users;
// using Documentmanager.Core.Domain.Repositories.Interfaces;

// namespace Documentmanager.Core.Domain.Services.Users
// {
//     public class TestService
//     {
//         public int LengthOfLongestSubstring(string s)
//         {
//             int n = s.Length;
//             int res = 0;
//             HashSet<char> set = new HashSet<char>();
//             for (int i = 0, j = 0; i < n; i++)
//             {
//                 while (j < n && !set.Contains(s[j]))
//                 {
//                     set.Add(s[j]);
//                     j++;
//                 }
//                 res = Math.Max(res, j - i);
//                 set.Remove(s[i]);
//             }
//             return res;
//         }
//         public int CharacterReplacement(string s, int k)
//     {
//         var res = 0;
//         var found = new Dictionary<char,int>();
//         var r = 0;
//         for (int l = 0; l < s.Length; l ++) {
//             while ( r < s.Length){
//                 if (!isValid(r+1, found.Values.Max(),k)){
//                     break;
//                 }
//                 if (!found.ContainsKey(s[r]))
//                 {
//                     found[s[r]] = 1;
//                 } else {
//                     found[s[r]]++;
//                 }
//                 r++;
//             }
//             res = Math.Max(res, r+1);
//             found[s[l]]--;
//         }
//         return res;
//     }

//         public int LeastInterval(char[] tasks, int n) {
//         var dict = new Dictionary<char, int>();
//         var queue = new Queue<int[]>();
//         var pq = new PriorityQueue<int,int>();
//         foreach(var c in tasks){
//             if (!dict.ContainsKey(c)){
//                 dict[c] = 1;
//             } else {
//                 dict[c]++;
//             }
//         }
//         foreach (var kvp in dict) {
//             pq.Enqueue(kvp.Value, -kvp.Value);
//         }
//         var time = 0;
//         while (pq.Count > 0 || queue.Count > 0){
//             time++;
//             var task = pq.Dequeue();
//             var taskWithNextTime = new int[]{task - 1, time+n};
//             queue.Enqueue(taskWithNextTime);
//             var peek = queue.Peek();
//             if (peek[1] == time) {
//                 var t = queue.Dequeue();
//                 pq.Enqueue(t, -t);
//             }
//         }
//         return time;
//     }
//         private bool isValid(int total, int mostUsed, int k) {
//             return (total - mostUsed) <= k;
//         }
//     }
// }
