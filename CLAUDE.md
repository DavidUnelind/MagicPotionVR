# MagicPotionVR

VR-spel för Meta Quest byggt i Unity 6000.3.6f1. Spelaren brygger trolldrycker och serverar gäster inom en tidsgräns.

## Teknisk stack
- Unity 6000.3.6f1
- Meta Quest (Oculus Interaction SDK används i koden)
- TextMeshPro för UI

## Projektstruktur
Egna scripts ligger i `Assets/Scripts/` och `Assets/liquid_feature/Scripts/`.

## Spelmekanik
- 120 sekunders tidsgräns
- Gäster (NPCs) vandrar till baren, väntar, och lämnar efter att ha blivit serverade
- Poäng räknas via `ServeButton` → `GameLoop`
- Vätskeimplementation med shader-baserad wobble + tilt-detektering för hällning
- Shaker kräver 3 sekunders skakning (mäts via hastighet) med progressbar-feedback

## Ansvarsområden i teamet
Linda arbetar med vätskeimplementation och effekter.

## Viktiga scripts
| Script | Ansvar |
|---|---|
| `GameLoop.cs` | Timer, poäng, game over |
| `Guest.cs` | NPC-rörelse, respawn med slumpad delay |
| `ServeButton.cs` | Trigger för servering |
| `Shaker.cs` | Skakmeknik med progressbar, reset vid respawn |
| `Tilt_detection.cs` | Detekterar flasktilt och startar/stoppar ström |
| `Wobble.cs` | Shader-baserad vätskewobble baserat på rörelse |
| `Stream.cs` | Vätskestråle vid hällning |

## Kom ihåg
- Du har tillgång till hela Unity-projektet — läs koden innan du frågar om saker som går att ta reda på själv.
