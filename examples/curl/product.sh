#!/usr/bin/env bash
# Oscaro Product API – cURL example
# Usage: PILOTERR_API_KEY=your-key ./product.sh
set -euo pipefail

curl -sS -G "https://api.piloterr.com/v2/oscaro/product" \
  -H "x-api-key: ${PILOTERR_API_KEY}" \
  --data-urlencode query=https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p
