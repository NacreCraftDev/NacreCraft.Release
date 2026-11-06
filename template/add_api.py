#!/usr/bin/env python3
"""Add a catalog entry together with its example file.

Usage:
    python add_api.py <id> --title "Title" --summary "Summary"
    python add_api.py <id> --title "Title" --summary "Summary" --template examples/Other.cs

<id> is the full api name, for example NacreCraft.ModApi.Wrapper.NcServer.
Without --template the file name comes from the last segment of the id and
lands under examples/.

The catalog is edited as plain text so comments and field order survive.
Because entries are appended at the end, `apis:` has to stay the last top
level section of NacreCraftTemplate.yaml.
"""

import argparse
import pathlib
import re
import sys

ROOT = pathlib.Path(__file__).resolve().parent
CATALOG = ROOT / "NacreCraftTemplate.yaml"

# Skeleton for a brand new example. Braces are doubled for str.format.
# The two placeholders are filled in by `ncm template example`, not here.
SKELETON = """// {title}
//
// {summary}
//
// The namespace and class below are filled in by `ncm template example`,
// using the project you run it in.
{usings}
namespace __MOD_NAMESPACE__;

public static class __MOD_CLASS__
{{
    // TODO: show how {api_id} is used.
    public static void Run()
    {{
    }}
}}
"""


def pick_using(api_id: str) -> str:
    """Return the using line that matches the api namespace."""
    if ".Wrapper." in api_id:
        return "using NacreCraft.ModApi.Wrapper;\n"
    if ".Extension." in api_id:
        return "using NacreCraft.ModApi.Extension;\n"
    return ""


def skeleton(api_id: str, title: str, summary: str) -> str:
    """Build the starter file for an api that has no example yet."""
    return SKELETON.format(
        title=title,
        summary=summary,
        usings=pick_using(api_id),
        api_id=api_id,
    )


def yaml_value(value: str) -> str:
    """Quote a scalar when plain text would confuse the parser."""
    if value != value.strip() or any(ch in value for ch in (":", "#", '"', "'")):
        return '"' + value.replace("\\", "\\\\").replace('"', '\\"') + '"'
    return value


def apis_is_last(text: str) -> bool:
    """True when no top level key follows the apis section."""
    seen = False
    for line in text.splitlines():
        if line.startswith("apis:"):
            seen = True
            continue
        if seen and line and not line[0].isspace() and not line.startswith("#"):
            return False
    return seen


def main() -> int:
    parser = argparse.ArgumentParser(description="Add a template entry and its example file.")
    parser.add_argument("id", help="full api name, for example NacreCraft.ModApi.Wrapper.NcServer")
    parser.add_argument("--title", required=True, help="short name shown in the list")
    parser.add_argument("--summary", required=True, help="one line description")
    parser.add_argument("--template", help="example path relative to the repository root")
    parser.add_argument("--members", help="comma separated member names, used by the usage check")
    args = parser.parse_args()

    text = CATALOG.read_text(encoding="utf-8")

    if not apis_is_last(text):
        print("error: apis: has to be the last top level section of NacreCraftTemplate.yaml", file=sys.stderr)
        return 1

    if re.search(rf"^\s*-\s*id:\s*{re.escape(args.id)}\s*$", text, re.MULTILINE):
        print(f"error: {args.id} is already in the catalog", file=sys.stderr)
        return 1

    template = args.template or f"examples/{args.id.rsplit('.', 1)[-1]}.cs"
    template_path = ROOT / template

    if not template_path.exists():
        template_path.parent.mkdir(parents=True, exist_ok=True)
        template_path.write_text(skeleton(args.id, args.title, args.summary), encoding="utf-8")
        print(f"created {template}")

    lines = text.rstrip("\n").split("\n")
    # an empty inline list cannot take block items, turn it into a block
    if lines[-1].strip() == "apis: []":
        lines[-1] = "apis:"

    lines.append(f"  - id: {yaml_value(args.id)}")
    lines.append(f"    title: {yaml_value(args.title)}")
    lines.append(f"    summary: {yaml_value(args.summary)}")
    lines.append(f"    template: {yaml_value(template)}")
    members = [name.strip() for name in (args.members or "").split(",") if name.strip()]
    if members:
        lines.append(f"    members: [{', '.join(members)}]")
    CATALOG.write_text("\n".join(lines) + "\n", encoding="utf-8")

    print(f"added {args.id} -> {template}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
