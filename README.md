# CsProjComparer
Compare two C# project files for similar content. This is useful when you maintain a shared library with older versions of visual studio or across different .NET framework versions. VS2015 has the "Shared Library" as a feature. However, previously you would create a VS20XX project, add some code, create the accompanying VS20YY project, manually copy that to the same folder (such that both csproj files are side by side), and add the "existing" code files. Your goal is to allow edits on both ends (VS20XX and YY) to be reflected in a single file.

At some point you or one of your team mates adds a file to one project but forgets the other project. Ideally, you want to have a compile error in this situation, which would prevent the commit... and you don't have to pull your hair :)

And thats why you compile this tool and integrate it as a post build task in both projects. Voila, consistancy!
