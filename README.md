# CsProjComparer
Compare two C# project files for similar content. This is useful when you maintain a shared library with older versions of visual studio or across different .NET framework versions. VS2015 has the "Shared Library" as a feature. However, previously you would create a VS20XX project, add some code, create the accompanying VS20YY project, manually copy that to the same folder (such that both csproj files are side by side), and add the "existing" code files. Your goal is to allow edits on both ends (VS20XX and YY) to be reflected in a single file.

At some point you or one of your team mates adds a file to one project but forgets the other project. Ideally, you want to have a compile error in this situation, which would prevent the commit... and you don't have to pull your hair :)

And thats why you compile this tool and integrate it as a post build task in both projects. Voila, consistancy!

### How it Works
 It takes two arguments, the .csproj files in question.

    CsProjComparer.exe "some path/first.csproj" "some path/second.csproj"

If both projects have the same `None`, `Compile`, `Content`, and `EmbeddedResource` elements in their respective XML, the tool is completely silent and returns exit code 0. Otherwise, differences are written in a human-readable format to the console and a non-zero exit code is returned, which effectively makes the build fail. Check the build output to see whats wrong.

### Visual Studio Integration as Pre-/Post-Build Task
Copy the exe to some folder relative to the solution (e.g. repository-root/tools).
In both visual studio projects add the above command as a post/pre build task. Make sure to add the solution relative path to the tool (VS has macros for that).
