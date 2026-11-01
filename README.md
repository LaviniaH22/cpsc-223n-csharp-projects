# CPSC 223N — Visual C# Programming

A Spring 2026 coursework collection of desktop applications and interactive motion simulations built by **Lavinia Hawkins** for **CPSC 223N at California State University, Fullerton — Spring 2026**.

These projects explore event-driven programming, user input, custom drawing, timers, and motion in C#.

## Explore the projects

| Project | What it demonstrates |
| --- | --- |
| [Imperial to Metric Converter](projects/imperial-metric-converter) | Convert inches to meters with a Windows Forms interface, input feedback, and Compute/Clear controls. |
| [Ninety-Degree Motion](projects/ninety-degree-motion) | Animate a ball along a path with a ninety-degree turn, user-selected speed, and start/pause controls. |
| [Ricochet Simulator](projects/ricochet-simulator) | Explore ball motion with configurable initial position, direction, speed, and live speed adjustment. |
| [Collision Detection](projects/collision-detection) | Simulate two moving balls with configurable speed and direction, live coordinates and distance, collision detection, and replay controls. |
| [Ellipse Motion Simulator](projects/ellipse-motion-simulator) | Animate a ball along an elliptical path with adjustable speed, pause/resume controls, and live position updates. |

## Build and run

The original projects use **C#**, **Windows Forms**, **System.Drawing**, and **Mono**. Each project is independent and includes its original build/run script.

1. Install Mono with `mcs`, `mono`, and Windows Forms support.
2. Open a terminal in the project folder.
3. Run the script listed in that project's README.

A graphical desktop session is required. These are desktop applications, not browser applications.

## Validation

The source in each project was checked with the Windows .NET Framework C# compiler during portfolio preparation. This is a compilation check; interactive behavior has not been retested as part of this cleanup.

## Coursework and attribution

These are academic projects, including adaptations of course examples. Instructor attribution and existing copyright/license notices are retained in the source. In particular, the Ricochet entry point includes Floyd Holliday's 2017 GPLv3 teaching example. See [ATTRIBUTION.md](ATTRIBUTION.md) for details.

The original project filenames are preserved so the supplied build scripts continue to refer to the same files. Student identifiers were removed from the publishing copies.

