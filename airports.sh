#!/bin/bash
set -o nounset -o errexit
# https://datahub.io/core/airport-codes
# Man kunne overveje at snuppe dataene direkte fra: https://ourairports.com/data/

# .name -> .Name
# .continent -> .Continent
# .iata_code != null
# .coordinates -> .Latitude og .Longitude
# .iata_code -> .IATA
# Drop resten og fjern dubletter
curl -vLs "https://datahub.io/core/airport-codes/r/airport-codes.json" | jq 'map(select(.iata_code != null) | . + (.coordinates | split(", ") | {Latitude: .[0] | tonumber, Longitude: .[1] | tonumber}) | {Name: .name, Continent: .continent, Latitude, Longitude, IATA: .iata_code}) | unique_by(.IATA)'
