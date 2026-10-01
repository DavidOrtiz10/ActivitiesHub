# EventsHub Web

React frontend for EventsHub. It currently shows a single page that loads all events from the API and lists their titles.

**Stack:** React 19, TypeScript 6, Vite 8, MUI 9 (Material UI + Emotion, Roboto font), axios. The React Compiler is enabled through a Babel preset.

## Prerequisites

- Node.js (developed with v24) and npm
- [EventsHub.Api](../../src/EventsHub.Api/README.md) running on `https://localhost:5001`

## Install

Install dependencies in **both** folders. `axios` is declared in `web/package.json`, one level up, and resolves from `web/node_modules`:

```powershell
cd web
npm install
cd EventsHub
npm install
```

## Scripts

Run from `web/EventsHub`:

| Command | What it does |
|---|---|
| `npm run dev` | Starts the Vite dev server on **https://localhost:3000** |
| `npm run build` | Type-checks (`tsc -b`) and builds to `dist/` |
| `npm run preview` | Serves the production build locally |
| `npm run lint` | Runs ESLint (TypeScript, react-hooks and react-refresh rules) |

The dev server uses `vite-plugin-mkcert` to create and trust a local HTTPS certificate. The first run may ask for permission to install the certificate authority.

Port 3000 matters: the API's CORS policy only allows `http(s)://localhost:3000`.

## How it talks to the API

`src/App.tsx` requests `https://localhost:5001/api/v1/events` with axios in a `useEffect` and stores the result in component state. The URL is hard-coded; there is no environment variable or shared API client yet.

The response type is `Activity`, declared in `src/lib/types/index.d.ts`. That file is a global ambient declaration, so `Activity` can be used without an import. It mirrors the backend `Event` entity with camelCase fields, with one difference: `latitude` and `longitude` are typed as `number` here, but the API sends them as strings.

## Structure

```
src/
  main.tsx              Entry point: StrictMode, global CSS, Roboto weights
  App.tsx               Event list page
  lib/types/index.d.ts  Global API types
  index.css, App.css    Styles
  assets/               Static images
```
