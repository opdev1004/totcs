# 📜 Tot file format rules

1. We must put data in tags like this:

```
<d:TagName>Data</d:TagName>
```

2. We must not wrap data tag with other data tag

```
// Do not do this, by not doing it we can avoid many misunderstanding of this world
<d:TagName1><d:TagName2>Data</d:TagName2></d:TagName1>

// Instead put JSON or XML data inside
<d:TagName1>XML</d:TagName1><d:TagName2>JSON</d:TagName2>
```

2. When we remove data in the future, we mark remove like this:

```
// This will be removed when file is cleaned.
// clean() will only maintain <d:TagName>Data</d:TagName>.
// clean() will remove <r:TagName>Data</r:TagName> and other strings that is not in tags of <d:>
<r:TagName>Data</r:TagName>
```

3. We must not use these characters in tags:

```
:
<
>
```

4. We must not use these in data:

```
<d:
<d:TagName>
</d:
</d:TagName>
<r:
<r:TagName>
</r:
</r:TagName>
```

5. Escaping. You can use entities or use your own escaping mechanism.

```
// our escaping in manual typing
<\d:
<\/d:
// They are going to converted for you when you call get function.
<\d: -> <d:
<\/d: -> </d:

//If you use it within programming language, string:
// our escaping
<\\d:
<\\/d:
// They are going to converted for you when you call get function.
<\\d: -> <d:
<\\/d: -> </d:
```

6. The structure of format shound not be changed by version of format.
7. By keeping all of the rules this format itself will be straight forward and flexible until our technology makes a big change
