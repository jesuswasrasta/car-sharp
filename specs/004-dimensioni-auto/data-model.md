# Modello Dati: Fase 4 – Tipologie e dimensioni

## Relazioni

```text
RichiestaNoleggio (Comando)
   └── PostiMinimi: int
   └── IdAuto: Guid? (Opzionale)

Auto (Entità/Valore)
   └── Capacita: int (> 0)
   └── Stato: Disponibile | Noleggiata
```

## Invarianti di Dominio

| ID | Invariante | Descrizione |
|----|------------|-------------|
| INV-001 | Capacità Positiva | Ogni mezzo deve avere una `Capacita` maggiore di zero. |
| INV-002 | Soddisfacimento Requisito | Un noleggio ha successo solo se `Auto.Capacita >= Richiesta.PostiMinimi`. |
| INV-003 | Vincolo Combinato | Se la richiesta specifica sia `IdAuto` che `PostiMinimi`, entrambi i vincoli devono essere soddisfatti. |

## Transizioni di Stato con Vincoli

La logica di transizione ora include una guardia basata sulla capacità.

```text
[Auto (Disponibile)] ── Noleggia(posti) ──► SE capacita >= posti ──► [Auto (Noleggiata)]
                                          └─ ALTRIMENTI ─────────► ERRORE
```

In OOP, la ricerca avviene tramite filtraggio della lista interna. In FP, viene introdotta l'entità `RichiestaNoleggio` come record per uniformare l'input delle pipeline di trasformazione.
