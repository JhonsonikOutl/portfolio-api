using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Mappers
{
    /// <summary>
    /// Mapea entre la entidad Domain SessionSettings y el modelo de persistencia MongoDB
    /// </summary>
    public static class SessionSettingsMapper
    {
        public static SessionSettings ToEntity(SessionSettings model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new SessionSettings
            {
                Id = model.Id,
                InactivityTimeoutMinutes = model.InactivityTimeoutMinutes,
                WarningBeforeTimeoutMinutes = model.WarningBeforeTimeoutMinutes,
                IsEnabled = model.IsEnabled,
                UpdatedAt = model.UpdatedAt
            };
        }

        public static SessionSettings ToModel(SessionSettings entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new SessionSettings
            {
                Id = entity.Id,
                InactivityTimeoutMinutes = entity.InactivityTimeoutMinutes,
                WarningBeforeTimeoutMinutes = entity.WarningBeforeTimeoutMinutes,
                IsEnabled = entity.IsEnabled,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static IEnumerable<SessionSettings> ToEntities(IEnumerable<SessionSettings> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            return models.Select(ToEntity);
        }

        public static IEnumerable<SessionSettings> ToModels(IEnumerable<SessionSettings> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return entities.Select(ToModel);
        }
    }
}
