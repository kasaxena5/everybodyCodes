---
name: c-sharp-guide
description: "Use when answering C# syntax questions, explaining language features, or providing C# examples for this project."
---

# C# Syntax Guide

1. Check the owning project's target framework and `LangVersion`, including inherited build settings. This project currently uses .NET 10 and C# 14 (the framework default), with nullable reference types and implicit usings enabled.
2. Answer using syntax supported by that effective C# version. Do not assume the installed SDK changes the language version or suggest preview features.
3. Give a brief explanation and the smallest useful, valid C# example. Follow the surrounding code's style and account for nullable references and implicit usings.
4. Distinguish language syntax from framework/library APIs. If a feature needs a newer version, state the requirement and offer a compatible alternative.
5. If syntax or version support is uncertain, consult the official Microsoft C# documentation before answering. Do not edit project code unless requested.
