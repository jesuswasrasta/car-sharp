# Modello Dati: Fase 2 – Identità e Stato

## Tipi Comuni
```csharp
public enum StatoAuto
{
    Disponibile,
    Noleggiata
}
```

## Percorso OOP (CarSharp.Oop)
### Classe `Auto`
- **Identità**: `public Guid Id { get; }` (Assegnato nel costruttore)
- **Dominio**: `public string Targa { get; private set; }`
- **Stato**: `public StatoAuto Stato { get; private set; }`
- **Metodi**:
    - `void Noleggia()`: Se `Stato == Noleggiata` lancia `InvalidOperationException`. Altrimenti `Stato = Noleggiata`.
    - `void Restituisci()`: `Stato = Disponibile`.

## Relazioni

```text
ParcoMezzi ───── 1 : N ─────► Auto
                               │
                               └── StatoAuto (Disponibile | Noleggiata)
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Identità Stabile | Il `Guid Id` non deve mai cambiare, indipendentemente dai cambiamenti della `Targa` o dello `Stato`. |
| INV-002 | Targa Obbligatoria | Un'auto non può esistere senza una `Targa` valida (non nulla e non vuota). |
| INV-003 | Esclusività Noleggio | Un'auto non può essere noleggiata se il suo stato corrente è già `Noleggiata`. |

## Transizioni di Stato

Il ciclo di vita operativo dell'auto è modellato tramite una macchina a stati finiti.

```text
[Disponibile] ──── Noleggia() ────► [Noleggiata]
      ▲                                   │
      └────────── Restituisci() ──────────┘
```

In OOP, queste transizioni sono mutazioni interne all'oggetto. In FP, sono trasformazioni che producono un nuovo tipo (AutoDisponibile → AutoNoleggiata).