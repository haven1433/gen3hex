﻿using HavenSoft.HexManiac.Core.Models.Runs;
using System.Windows.Input;

namespace HavenSoft.HexManiac.Core.ViewModels.Tools {
   public class TrainerPokemonTeamElementViewModel : TextStreamElementViewModel {
      private StubCommand setDefaultMoves, setDefaultItems;
      public ICommand SetDefaultMoves => StubCommand(ref setDefaultMoves, ExecuteSetDefaultMoves);
      public ICommand SetDefaultItems => StubCommand(ref setDefaultItems, ExecuteSetDefaultItems);

      public TrainerPokemonTeamRun Run { get; private set; }

      public TrainerPokemonTeamElementViewModel(ViewPort viewPort, TrainerPokemonTeamRun tptRun, int itemAddress)
      : base(viewPort, itemAddress, tptRun.FormatString) {
         Run = tptRun;
      }

      protected override bool TryCopy(StreamElementViewModel other) {
         if (!(other is TrainerPokemonTeamElementViewModel that)) return false;
         setDefaultMoves = that.setDefaultMoves;
         setDefaultItems = that.setDefaultItems;
         Run = that.Run;
         return base.TryCopy(other);
      }

      private void ExecuteSetDefaultMoves() {
         var result = Run.DeserializeRun(Content, ViewPort.CurrentChange, true, false);
         HandleNewDataStream(Run, result);
         ViewPort.Tools.TableTool.DataForCurrentRunChanged();
      }

      private void ExecuteSetDefaultItems() {
         var result = Run.DeserializeRun(Content, ViewPort.CurrentChange, false, true);
         HandleNewDataStream(Run, result);
         ViewPort.Tools.TableTool.DataForCurrentRunChanged();
      }
   }
}
