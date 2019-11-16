//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity lifeTimerEntity { get { return GetGroup(GameMatcher.LifeTimer).GetSingleEntity(); } }
    public GameScene.ECS.Components.LifeTimerComponent lifeTimer { get { return lifeTimerEntity.lifeTimer; } }
    public bool hasLifeTimer { get { return lifeTimerEntity != null; } }

    public GameEntity SetLifeTimer(float newValue) {
        if (hasLifeTimer) {
            throw new Entitas.EntitasException("Could not set LifeTimer!\n" + this + " already has an entity with GameScene.ECS.Components.LifeTimerComponent!",
                "You should check if the context already has a lifeTimerEntity before setting it or use context.ReplaceLifeTimer().");
        }
        var entity = CreateEntity();
        entity.AddLifeTimer(newValue);
        return entity;
    }

    public void ReplaceLifeTimer(float newValue) {
        var entity = lifeTimerEntity;
        if (entity == null) {
            entity = SetLifeTimer(newValue);
        } else {
            entity.ReplaceLifeTimer(newValue);
        }
    }

    public void RemoveLifeTimer() {
        lifeTimerEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GameScene.ECS.Components.LifeTimerComponent lifeTimer { get { return (GameScene.ECS.Components.LifeTimerComponent)GetComponent(GameComponentsLookup.LifeTimer); } }
    public bool hasLifeTimer { get { return HasComponent(GameComponentsLookup.LifeTimer); } }

    public void AddLifeTimer(float newValue) {
        var index = GameComponentsLookup.LifeTimer;
        var component = CreateComponent<GameScene.ECS.Components.LifeTimerComponent>(index);
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceLifeTimer(float newValue) {
        var index = GameComponentsLookup.LifeTimer;
        var component = CreateComponent<GameScene.ECS.Components.LifeTimerComponent>(index);
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveLifeTimer() {
        RemoveComponent(GameComponentsLookup.LifeTimer);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherLifeTimer;

    public static Entitas.IMatcher<GameEntity> LifeTimer {
        get {
            if (_matcherLifeTimer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LifeTimer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLifeTimer = matcher;
            }

            return _matcherLifeTimer;
        }
    }
}
