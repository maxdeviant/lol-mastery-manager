LoL Mastery Manager
===================

[![Github Releases](https://img.shields.io/github/downloads/maxdeviant/lol-mastery-manager/latest/total.svg?style=flat-square)](https://github.com/maxdeviant/lol-mastery-manager/releases/latest)

A tool for managing mastery pages in League of Legends. Inspired by [Championify](https://github.com/dustinblackman/Championify).

![A pretty screenshot.](Screenshot.png)

[See it in action!](https://raw.githubusercontent.com/maxdeviant/lol-mastery-manager/master/Example%201%20-%20Menu.gif)

Features
--------
- [x] Download mastery pages from [Champion.gg](http://champion.gg/)
- [x] Automatically assign mastery points based on selected champion and role
- [x] Works in the menus and during champion select

FAQ
---

### Why is it not properly assigning masteries?
Calculating the positioning of the mastery trees is hard to get right for all screen sizes. I have seen it work very consistently on client sizes of 1280x800 or smaller. I will be working to improve behavior at all screen sizes.

### Will I get banned for using this?
Riot Sargonas has stated the following on the matter:

>This one is is in an gray area. It does a lot more interactions for the user than we typically consider ok, though the time and place it does it (masteries page) is a lot less sensitive than, say, in game.
It's not something we can make a snap judgement on at this time without careful consideration, but we're definitely not 100% okay with the idea yet due to the fact it's scripting player actions, even though in the out-of-game client. [[Link]](https://www.reddit.com/r/leagueoflegends/comments/3oeb8q/just_made_a_tool_for_automatically_creating/cvx7hm3)

>We typically don't ban for using a tool like this without an announcement. Further more, usage of these kinds of tools usually result in a warning and a temp ban before a permanent ban being the last step.
I can't give you any kind of update now or anytime in the immediate future, but I can say that you will not wake up to a sudden and un-expected ban without warning by using this tool. [[Link]](https://www.reddit.com/r/leagueoflegends/comments/3oeb8q/just_made_a_tool_for_automatically_creating/cw03o71)

### Can I run it on Mac OS X?
As of right now only Windows is supported, but OS X support is being investigated.
