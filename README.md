# Fly Transport A/S beregningssystem ✈️

Har du altid ønsket dig et simpelt program, der kan give dig et realistisk overslag™ på en flybillet? Så har du fundet det!

Med Fly Transport A/S beregningssystem er det legende let at udregne prisen for at rejse fra A til B med fly med en betydelig usikkerhed!

## Opbygning 🏗️

Fly Transport A/S bestillingssystem er udviklet i det populære programmeringsprog C# og .NET Core frameworket (3.1 LTS).

Målgruppen er end-users og målet er derfor ikke at udregne de faktiske omkostninger, men at udregne en mere eller mindre realistisk billetpris, hvor der også er taget højde for at det er en forretning der skal tjenes på.

Programmet er udviklet som et OOP-program, hvor der bl.a. er brugt nedarvning. Et eksempel herpå kan ses i nedenstående figur:
```
           ,-----.
           |Plane|
           `--^--'
              |
      x-------x------- x
      |                |       
,-----------.   ,-------------.
|BusinessJet|   |CommericalJet|
`-----------'   `-------------'
```
`Plane`-klassen indeholder properties for alle relevante grundoplysninger (`Manufacturer`, `Speed` etc.), og nogle naive implementeringer der bl.a. kan udregne brændstofforbruget, omkostninger m.m.. `BusinessJet` og `CommericalJet` sætter alle relevante properties og indeholder nogle mere realistiske implementeringer, der tager højde for relevante faktorer, f.eks. må man antage at der er nogle andre omkostninger ved at tage et privatfly fremfor et rutefly.

## Hvordan bruger jeg det? ℹ️

[![asciicast](https://asciinema.org/a/FHPWKQ9bAT4h9T8tOP2F0mYIs.svg)](https://asciinema.org/a/FHPWKQ9bAT4h9T8tOP2F0mYIs)
