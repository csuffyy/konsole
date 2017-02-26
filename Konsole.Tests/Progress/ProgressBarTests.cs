﻿using System;
using System.Text;
using ApprovalTests;
using ApprovalTests.Maintenance;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Konsole.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class ProgressBarTests
    {
        [Test]
        public void EnsureNoAbandonedFiles()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles();
        }

        [Test]
        public void refresh_should_show_progress_title_and_progress_bar()
        {
            var testoutput = new StringBuilder();
            var console = new BufferedWriter(80,20);
            var pb = new ProgressBar(10, console);
            
            for (int i = 1; i < 5; i++)
            {
                System.Console.WriteLine(" --- test " + i + "-----");
                pb.Refresh(i, "ITEM " + i);
                Console.WriteLine(console.BufferWrittenString);
                testoutput.AppendLine(console.BufferWrittenString);
            }
            Console.WriteLine();
            Approvals.Verify(testoutput.ToString());
        }

        [Test]
        public void should_still_update_progress_even_when_writing_lines_after_progress_bar()
        {
            var console = new BufferedWriter(40,10);
            console.WriteLine("line 1");
            var pb = new ProgressBar(10, console);
            pb.Refresh(0, "loading");
            console.WriteLine("line 2");
            pb.Refresh(1, "cats");
            console.WriteLine("line 3");
            pb.Refresh(10, "dogs");
            console.WriteLine("line 4");

            Console.WriteLine(console.BufferWrittenString);
            Console.WriteLine();
            Approvals.Verify(console.BufferWrittenString);
        }




    }
}
