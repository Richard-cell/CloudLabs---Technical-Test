using System;
using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;

namespace SchoolNotesApp.NoteFeature.Validation
{
    public sealed class NoteValidationResult
    {
        public IReadOnlyList<Student> WronglyApproved { get; }
        public IReadOnlyList<Student> WronglyFailed { get; }
        public IReadOnlyList<Student> UngradedStudents { get; }
        public bool HasErrors => WronglyApproved.Count > 0 || WronglyFailed.Count > 0 || UngradedStudents.Count > 0;

        public NoteValidationResult(IReadOnlyList<Student> wronglyApproved, IReadOnlyList<Student> wronglyFailed, IReadOnlyList<Student> ungradedStudents)
        {
            WronglyApproved = wronglyApproved;
            WronglyFailed = wronglyFailed;
            UngradedStudents = ungradedStudents;
        }
    }
}