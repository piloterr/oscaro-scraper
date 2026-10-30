<p align="center">
  <a href="https://www.piloterr.com"><img src="images/banner.png" alt="Oscaro Scraper API by Piloterr – scrape Oscaro data as JSON" width="100%"></a>
</p>

# Oscaro Scraper API

[![Discord](https://img.shields.io/badge/Discord-Join%20the%20community-5865F2?logo=discord&logoColor=white)](https://discord.gg/CFj3ph8aHU) [![Get API key](https://img.shields.io/badge/Piloterr-Get%20a%20free%20API%20key-111827)](https://app.piloterr.com) [![API docs](https://img.shields.io/badge/Docs-OpenAPI-0EA5E9)](https://app.piloterr.com/openapi) [![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

The **Piloterr Oscaro Scraper API** lets you extract public data from Oscaro as **clean, structured JSON** — no proxies, headless browsers or HTML parsing to maintain. Endpoints available in production:

- **Oscaro Product API** — Oscaro Product API.
- **Oscaro Search API** — Oscaro Search API.

Use it for price monitoring, assortment tracking, competitor analysis, product research or catalog enrichment.

| Endpoint | Path | Credits | What you get |
|---|---|---|---|
| [Oscaro Product API](#oscaro-product-api) | `/v2/oscaro/product` | 2 | Oscaro Product API. Fetch full auto parts product data from an Oscaro product URL. Returns pricing, brand, reference, images and availability via browser rendering and JSON-LD parsing. |
| [Oscaro Search API](#oscaro-search-api) | `/v2/oscaro/search` | 2 | Oscaro Search API. Search auto parts by keyword, search URL or category listing on oscaro.com, oscaro.es and oscaro.pt. Returns prices and pagination via browser rendering. |

## Table of contents

- [How it works](#how-it-works)
- [Oscaro Product API](#oscaro-product-api)
- [Oscaro Search API](#oscaro-search-api)
- [Code examples in other languages](#code-examples-in-other-languages)
- [Practical use cases](#practical-use-cases)
- [Why Piloterr?](#why-piloterr)
- [FAQ](#faq)
- [Related scrapers](#related-scrapers)
- [Support](#support)

## How it works

1. **Create a free Piloterr account** and copy your API key from the [dashboard](https://app.piloterr.com).
2. **Send a `GET` (or `POST`) request** to `https://api.piloterr.com/v2/...` with the header `x-api-key: YOUR_API_KEY`.
3. **Receive structured JSON** — Piloterr handles rendering, proxies, retries and parsing.

```bash
export PILOTERR_API_KEY="your-api-key"
pip install requests        # Python
# TypeScript: Node.js 18+ (native fetch), run with `npx tsx file.ts`
```

> Every endpoint accepts both `GET` (query string) and `POST` (JSON body with the same fields).

## Oscaro Product API

Oscaro Product API. Fetch full auto parts product data from an Oscaro product URL. Returns pricing, brand, reference, images and availability via browser rendering and JSON-LD parsing.

`GET | POST https://api.piloterr.com/v2/oscaro/product` · **2 credits per request** · [Full reference](https://app.piloterr.com/openapi)

### Request parameters

| Parameter | Type | Required | Description |
|---|---|---|---|
| `query` | string | ✅ | Oscaro product page URL (`*-{sku}-p` suffix). |
| `return_page_source` | boolean | — | When `true`, return the raw HTML page source as `{"result": "..."}` instead of structured JSON. Defaults to `false`. |

### Python example

```python
import json
import os
import requests

response = requests.get(
    "https://api.piloterr.com/v2/oscaro/product",
    headers={"x-api-key": os.environ["PILOTERR_API_KEY"]},
    params={
        "query": "https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p",
    },
    timeout=60,
)
response.raise_for_status()
data = response.json()

print(json.dumps(data, indent=2, ensure_ascii=False)[:2000])
```

### TypeScript example

```typescript
type OscaroProduct = {
  url?: string;
  brand?: string;
  price?: number;
  title?: string;
  images?: string[];
  currency?: string;
  in_stock?: boolean;
  image_url?: string;
  reference?: string;
  product_id?: string;
};

const params = new URLSearchParams({
  "query": "https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p",
});

const response = await fetch(`https://api.piloterr.com/v2/oscaro/product?${params}`, {
  headers: { "x-api-key": process.env.PILOTERR_API_KEY! },
});
if (!response.ok) throw new Error(`Piloterr error ${response.status}: ${await response.text()}`);

const data = (await response.json()) as OscaroProduct;

console.log(JSON.stringify(data, null, 2).slice(0, 2000));
```

<details>
<summary><b>POST variant</b></summary>

```http
POST https://api.piloterr.com/v2/oscaro/product
x-api-key: YOUR_API_KEY
Content-Type: application/json

{"query": "https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p"}
```
</details>

### Output example

Example response from the API reference, abridged — real values depend on the page you scrape.

```json
{
  "url": "https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p",
  "brand": "MOTUL",
  "price": 13.28,
  "title": "MOTUL - SPECIFIC 0720 5W-30  1L - ref. 102208 - Huile moteur",
  "images": [
    "https://oscaro.media/catalog/images/oscjpg/normal/1114/specific 0720 5w30 1l.jpg"
  ],
  "currency": "EUR",
  "in_stock": true,
  "image_url": "https://oscaro.media/catalog/images/oscjpg/normal/1114/specific 0720 5w30 1l.jpg",
  "reference": "102208",
  "product_id": "15213891"
}
```

### Response fields

| Field | Type | Description |
|---|---|---|
| `url` | string |  |
| `brand` | string |  |
| `price` | number |  |
| `title` | string |  |
| `images` | string[] |  |
| `currency` | string |  |
| `in_stock` | boolean |  |
| `image_url` | string |  |
| `reference` | string |  |
| `product_id` | string |  |

## Oscaro Search API

Oscaro Search API. Search auto parts by keyword, search URL or category listing on oscaro.com, oscaro.es and oscaro.pt. Returns prices and pagination via browser rendering.

`GET | POST https://api.piloterr.com/v2/oscaro/search` · **2 credits per request** · [Full reference](https://app.piloterr.com/openapi)

### Request parameters

| Parameter | Type | Required | Description |
|---|---|---|---|
| `query` | string | ✅ | Search keyword, search URL, or category listing URL (`*-{id}-g`). |
| `region` | string | — | Target shop when `query` is a keyword (default: `com`). |
| `page` | number | — | 1-based page number override for category listing URLs (default: read from URL or `1`). Uses `?page=2` on `*-g` pages. |
| `return_page_source` | boolean | — | When `true`, return the raw HTML page source as `{"result": "..."}` instead of structured JSON. Defaults to `false`. |

### Python example

```python
import json
import os
import requests

response = requests.get(
    "https://api.piloterr.com/v2/oscaro/search",
    headers={"x-api-key": os.environ["PILOTERR_API_KEY"]},
    params={
        "query": "moteur ai",
    },
    timeout=60,
)
response.raise_for_status()
data = response.json()

print(json.dumps(data, indent=2, ensure_ascii=False)[:2000])
```

### TypeScript example

```typescript
type OscaroSearch = {
  results?: {
    url?: string;
    price?: number;
    title?: string;
    currency?: string;
    image_url?: string;
    product_id?: string;
  }[];
  pagination?: {
    next?: unknown;
    page?: number;
    per_page?: number;
    total_pages?: unknown;
    total_results?: unknown;
  };
};

const params = new URLSearchParams({
  "query": "moteur ai",
});

const response = await fetch(`https://api.piloterr.com/v2/oscaro/search?${params}`, {
  headers: { "x-api-key": process.env.PILOTERR_API_KEY! },
});
if (!response.ok) throw new Error(`Piloterr error ${response.status}: ${await response.text()}`);

const data = (await response.json()) as OscaroSearch;

console.log(JSON.stringify(data, null, 2).slice(0, 2000));
```

<details>
<summary><b>POST variant</b></summary>

```http
POST https://api.piloterr.com/v2/oscaro/search
x-api-key: YOUR_API_KEY
Content-Type: application/json

{"query": "moteur ai"}
```
</details>

### Output example

Example response from the API reference, abridged — real values depend on the page you scrape.

```json
{
  "results": [
    {
      "url": "https://www.oscaro.com/moteur-electrique-pulseur-dair-habitacle-magneti-marelli-069412247010-13361124-793-p",
      "price": 91.79,
      "title": "Moteur électrique, pulseur d'air habitacle MAGNETI MARELLI - 069412247010",
      "currency": "EUR",
      "image_url": "https://oscaro.media/catalog/images/jpg/normal/95/mt_mte247ax-wr.jpg",
      "product_id": "13361124-793"
    },
    {
      "url": "https://www.oscaro.com/moteur-electrique-pulseur-dair-habitacle-magneti-marelli-069412215010-13485530-793-p",
      "price": 47.6,
      "title": "Moteur électrique, pulseur d'air habitacle MAGNETI MARELLI - 069412215010",
      "currency": "EUR",
      "image_url": "https://oscaro.media/catalog/images/jpg/normal/95/mt_mte215ax_wr.jpg",
      "product_id": "13485530-793"
    }
  ],
  "pagination": {
    "next": null,
    "page": 1,
    "per_page": 50,
    "total_pages": null,
    "total_results": null
  }
}
```

### Response fields

| Field | Type | Description |
|---|---|---|
| `results[]` | object[] |  |
| `results[].url` | string |  |
| `results[].price` | number |  |
| `results[].title` | string |  |
| `results[].currency` | string |  |
| `results[].image_url` | string |  |
| `results[].product_id` | string |  |
| `pagination` | object |  |
| `pagination.next` | object |  |
| `pagination.page` | integer |  |
| `pagination.per_page` | integer |  |
| `pagination.total_pages` | object |  |
| `pagination.total_results` | object |  |

## Code examples in other languages

Ready-to-run examples live in the [`examples/`](examples) folder. Each one reads your key from the `PILOTERR_API_KEY` environment variable.

| Language | Product | Search |
|---|---|---|
| cURL (Bash) | [`product`](examples/curl/product.sh) | [`search`](examples/curl/search.sh) |
| PHP | [`product`](examples/php/product.php) | [`search`](examples/php/search.php) |
| C# | [`product`](examples/csharp/Product.cs) | [`search`](examples/csharp/Search.cs) |
| Go | [`product`](examples/go/product.go) | [`search`](examples/go/search.go) |
| Java | [`product`](examples/java/ProductExample.java) | [`search`](examples/java/SearchExample.java) |
| Ruby | [`product`](examples/ruby/product.rb) | [`search`](examples/ruby/search.rb) |
| Node.js | [`product`](examples/nodejs/product.mjs) | [`search`](examples/nodejs/search.mjs) |

## Practical use cases

- **Price monitoring** — Track prices, promotions and availability over time to react to competitors.
- **Assortment & catalog tracking** — Follow new products, delisted items and category changes.
- **Competitor analysis** — Benchmark ranges, brands and positioning across retailers.
- **Product research** — Spot best-sellers and trends from search results.
- **Catalog enrichment** — Pull titles, images and attributes into your PIM or marketplace listings.

## Why Piloterr?

- **No infrastructure** — no proxies, headless browsers or CAPTCHA solving to run yourself.
- **Structured JSON** — stable, documented fields instead of brittle HTML selectors.
- **Pay per request** — transparent credit cost per endpoint, failed requests are retried for you.
- **One API key, hundreds of sources** — Oscaro plus many other e-commerce, search and company data endpoints in the [Piloterr library](https://www.piloterr.com/library).
- **Any language** — plain HTTPS + JSON, with examples for Python, TypeScript, cURL, PHP, C#, Go, Java, Ruby and Node.js.

## FAQ

### Is it legal to scrape Oscaro?

Collecting publicly available data is generally allowed in many jurisdictions, but rules depend on your country, the data you collect and how you use it. Avoid personal data unless you have a lawful basis, respect the website's terms where they apply, and get legal advice for your specific use case. This is not legal advice.

### Do I need proxies or a headless browser?

No. Piloterr handles rendering, proxy rotation and retries on its side. You only send an HTTPS request with your `x-api-key` header and receive JSON.

### How much does a request cost?

Credits per call: `/v2/oscaro/product` **2**, `/v2/oscaro/search` **2**. See the [Piloterr website](https://www.piloterr.com) for plans.

### What data does the Oscaro API return?

Each endpoint returns structured JSON: **Oscaro Product** — `url`, `brand`, `price`, `title`, `images`, `currency`; **Oscaro Search** — `results[]`, `results[].url`, `results[].price`, `results[].title`, `results[].currency`, `results[].image_url`.

### How do I paginate Oscaro results?

Use the `page` parameter and keep requesting while the response indicates more results.

### Can I target a specific country or language?

Yes, use `region` — see the parameter tables above for accepted values.

### Can I get the raw HTML instead of JSON?

Yes. Set `return_page_source=true` and the response becomes `{"result": "..."}` with the page source.

### Which languages are supported?

Any language that can make an HTTP request. This README shows Python and TypeScript; the [`examples/`](examples) folder contains cURL, PHP, C#, Go, Java, Ruby and Node.js.

## Related scrapers

- [About You Scraper API](https://github.com/piloterr/aboutyou-scraper)
- [Action Scraper API](https://github.com/piloterr/action-scraper)
- [Aldi Scraper API](https://github.com/piloterr/aldi-scraper)
- [Alibaba Scraper API](https://github.com/piloterr/alibaba-scraper)
- [AliExpress Scraper API](https://github.com/piloterr/aliexpress-scraper)
- [Allegro Scraper API](https://github.com/piloterr/allegro-scraper)
- [All Piloterr scrapers on GitHub](https://github.com/piloterr)

## Support

- 💬 Join the [Piloterr Discord](https://discord.gg/CFj3ph8aHU) to ask questions and share what you build.
- 📚 Browse the [API reference (OpenAPI)](https://app.piloterr.com/openapi) and the [Piloterr library](https://www.piloterr.com/library).
- 🔑 [Get your API key](https://app.piloterr.com).

---

<p align="center">
  <a href="https://www.piloterr.com">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="images/logo-dark.svg">
      <img src="images/logo.svg" alt="Piloterr" height="32">
    </picture>
  </a>
  <br>
  <sub>Built by <a href="https://www.piloterr.com">Piloterr</a> — web scraping APIs for e-commerce, search, real estate and more, one API key.</sub>
</p>

<sub>Oscaro is a trademark of its respective owner. This repository is not affiliated with or endorsed by Oscaro. Examples are released under the MIT license.</sub>
