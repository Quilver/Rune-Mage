---
name: wiki
id: wiki
description: This is an LLM-maintained knowledge base on [Rune Mage]. The LLM writes and maintains all files under `LLM/wiki/`. The human curates raw sources and directs queries. The human never edits wiki files directly.
---
# Directory Layout

- `LLM/wiki/index.md` — Master catalog. Every wiki page must appear here.
- `LLM/wiki/log.md` — Append-only activity log.
- `LLM/wiki/summaries/` — One summary page per raw source document.
- `LLM/wiki/concepts/` — Concept, strategy, and framework pages.
- `LLM/wiki/entities/` — Entity pages (people, tools, organizations, products — whatever "things" exist in your domain).
- `LLM/wiki/syntheses/` — Comparison tables, decision frameworks, cross-cutting analyses.
- `LLM/wiki/journal/` — Research or session journal entries.
- `LLM/wiki/presentations/` — Marp slide decks generated from wiki content.

# File Naming

- All lowercase, hyphens for word separation: `concept-name.md`
- No spaces, no special characters, no uppercase
- Name should match the page title slug

# Page Format

Every wiki page uses this frontmatter and structure:

```yaml
---
title: "Page Title"
type: concept | entity | summary | synthesis
tags: [tag1, tag2, tag3]
created: YYYY-MM-DD
updated: YYYY-MM-DD
sources: ["raw/filename.txt"]
confidence: high | medium | low
---
```

## Required Sections by Page Type

**Summary pages** (`wiki/summaries/`):
- `## Key Points` — Bulleted list of main claims/ideas
- `## Relevant Concepts` — Links to concept pages this source touches
- `## Source Metadata` — Type of source, author/speaker, date, URL or identifier

**Concept pages** (`wiki/concepts/`):
- `## Definition` — One-paragraph plain-English definition
- `## How It Works` — Mechanics, process, or structure of the concept
- `## Key Parameters` — Important variables, dimensions, or factors
- `## When To Use` — Situations and contexts where this concept applies
- `## Risks & Pitfalls` — Known failure modes, common mistakes, limitations
- `## Related Concepts` — Wiki links to related pages
- `## Sources` — Which raw sources inform this page

**Entity pages** (`wiki/entities/`):
- `## Overview` — What this entity is
- `## Characteristics` — Key properties, attributes, structure
- `## Common Strategies` — Links to concept pages for strategies or methods associated with this entity
- `## Related Entities` — Links to related entity pages

**Synthesis pages** (`wiki/syntheses/`):
- `## Comparison` — Table or structured comparison
- `## Analysis` — Cross-cutting insights
- `## Recommendations` — When to prefer which approach
- `## Pages Compared` — Links to all pages involved

# Linking Conventions

- Use Obsidian-style wiki links: `[[concepts/concept-name]]`
- Always use relative paths from wiki root
- Every page must link to at least one other page (no orphans)
- When mentioning a concept that has a page, always link it

# Tagging Taxonomy

<!-- CUSTOMIZE: Replace these placeholder categories with tags relevant to your domain. -->
<!-- Each category should have 3-8 specific tags. -->
<!-- Example for a cooking KB: -->
<!--   Cuisine: italian, japanese, french, mexican -->
<!--   Technique: braising, fermenting, sous-vide, grilling -->
<!--   Ingredient: protein, vegetable, grain, dairy -->

- **Feature**: `level-layout`, `spells`, `Enemies`
- **Test cases**: `example`, `edge-cases`, `bad`
- **Implementation**: `Interfaces`, `AI`, `unit-tests`
- **Scope**: `foundational`, `advanced`, `experimental`
- **Status**: `well-established`, `emerging`, `speculative`

# Confidence Levels

- **high** — Well-established idea, multiple corroborating sources, demonstrated with concrete examples
- **medium** — Supported by sources but limited examples or single-source
- **low** — Single mention, anecdotal, or speculative

# Workflows

## Ingest

When the user says "ingest [source]":

1. Read the raw source completely
2. Create `wiki/summaries/<source-slug>.md` with full summary
3. Identify all concepts, entities, and strategies mentioned
4. For each concept/entity: create the page if it doesn't exist, or update it with new information if it does
5. Add cross-links in both directions between all touched pages
6. Update `wiki/index.md` — add new entries, update summaries of changed pages
7. Append to `wiki/log.md` with timestamp, source name, pages created/updated
8. Flag any contradictions with existing wiki content

## Query

When the user asks a question:

1. Read `wiki/index.md` to find relevant pages
2. Read those pages
3. Synthesize an answer citing specific pages with wiki links
4. If the answer reveals new insight worth preserving:
   - Create a synthesis page in `wiki/syntheses/`
   - Update index and log

## Lint

When the user says "lint" or "health check":

1. Read all wiki pages
2. Check for: orphan pages (no inbound links), stale claims, contradictions between pages, missing cross-links, incomplete sections, low-confidence pages that could be strengthened
3. Fix what can be fixed automatically
4. Report issues that need human judgment
5. Suggest new sources or topics to investigate
6. Update log

# Rules

- Never modify files in `raw/`
- Always update `index.md` and `log.md` after any wiki change
- Prefer updating existing pages over creating duplicates
- When in doubt about a claim, set confidence to "low" and note the uncertainty
- Keep pages focused — one concept per page, split if a page gets too long
- Use plain English — define jargon on first use in each page
- All dates in ISO 8601 format: YYYY-MM-DD
- When a source provides specific examples, include them with concrete details
