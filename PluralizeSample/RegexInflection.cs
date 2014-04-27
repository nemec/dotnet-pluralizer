using System;
using System.Text.RegularExpressions;

namespace PluralizeSample
{
    public class RegexInflection : IInflection
    {
        private readonly Regex _regex;

        private readonly string _groupReplacement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralizeSample.RegexInflection"/> class.
        /// </summary>
        /// <param name='regex'>
        /// Regex.
        /// </param>
        /// <param name='groupReplacement'>
        /// If regex contains a group, the matched group will be replaced
        /// with this text. If not, this text will be appended to the end
        /// of the matched string.
        /// </param>
        public RegexInflection(string regex, string groupReplacement)
        {
            _regex = new Regex(regex + "$");
            _groupReplacement = groupReplacement;
        }

        public bool TryInflect(string word, out IPluralForm result)
        {
            var match = _regex.Match(word);
            if(match.Success)
            {
                if(match.Groups.Count == 1)
                {
                    result = new SimplePluralForm(word + _groupReplacement, RuleDisplay);
                }
                else
                {
                    var mgrp = match.Groups[1];
                    var start = word.Substring(0, mgrp.Index);
                    var end = word.Substring(mgrp.Index + mgrp.Length);
                    result = new SimplePluralForm(start + _groupReplacement + end, RuleDisplay);
                }
                return true;
            }
            result = null;
            return false;
        }

        public string RuleDisplay
        {
            get
            {
                if(_regex.GetGroupNumbers().Length == 1)
                {
                    return String.Format("regex: {0} -> {0}{1}", _regex.ToString(), _groupReplacement);
                }
                var rx = _regex.ToString();
                return String.Format("regex: {0} -> {1}{2}", rx,
                    rx.Substring(0, rx.IndexOf("(")), _groupReplacement);
            }
        }
    }
}

