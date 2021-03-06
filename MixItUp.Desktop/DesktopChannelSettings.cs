﻿using Mixer.Base.Model.Channel;
using Mixer.Base.Model.OAuth;
using MixItUp.Base;
using MixItUp.Base.Actions;
using MixItUp.Base.Commands;
using MixItUp.Base.Model.Favorites;
using MixItUp.Base.Model.Interactive;
using MixItUp.Base.Model.Overlay;
using MixItUp.Base.Model.Remote.Authentication;
using MixItUp.Base.Model.Serial;
using MixItUp.Base.Model.User;
using MixItUp.Base.Remote.Models;
using MixItUp.Base.Services;
using MixItUp.Base.Util;
using MixItUp.Base.ViewModel.Interactive;
using MixItUp.Base.ViewModel.Requirement;
using MixItUp.Base.ViewModel.User;
using MixItUp.Desktop.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MixItUp.Desktop
{
    [DataContract]
    public class DesktopSavableChannelSettings : ISavableChannelSettings
    {
        public const int LatestVersion = 28;

        [JsonProperty]
        public int Version { get; set; } = DesktopChannelSettings.LatestVersion;

        [JsonProperty]
        public bool LicenseAccepted { get; set; }

        [JsonProperty]
        public bool OptOutTracking { get; set; }

        [JsonProperty]
        public bool ReRunWizard { get; set; }
        [JsonProperty]
        public bool DiagnosticLogging { get; set; }

        [JsonProperty]
        public bool IsStreamer { get; set; }

        [JsonProperty]
        public OAuthTokenModel OAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel BotOAuthToken { get; set; }

        [JsonProperty]
        public OAuthTokenModel StreamlabsOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel GawkBoxOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel TwitterOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel SpotifyOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel DiscordOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel TiltifyOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel TipeeeStreamOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel TreatStreamOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel StreamJarOAuthToken { get; set; }
        [JsonProperty]
        public OAuthTokenModel PatreonOAuthToken { get; set; }

        [JsonProperty]
        public Dictionary<string, CommandGroupSettings> CommandGroups { get; set; }
        [JsonProperty]
        public Dictionary<string, HotKeyConfiguration> HotKeys { get; set; }

        [JsonProperty]
        public ExpandedChannelModel Channel { get; set; }

        [JsonProperty]
        public bool FeatureMe { get; set; }
        [JsonProperty]
        public StreamingSoftwareTypeEnum DefaultStreamingSoftware { get; set; } = StreamingSoftwareTypeEnum.OBSStudio;
        [JsonProperty]
        public string DefaultAudioOutput { get; set; }
        [JsonProperty]
        public bool SaveChatEventLogs { get; set; }

        [JsonProperty]
        public bool WhisperAllAlerts { get; set; }
        [JsonProperty]
        public bool LatestChatAtTop { get; set; }
        [JsonProperty]
        public bool HideViewerAndChatterNumbers { get; set; }
        [JsonProperty]
        public bool HideChatUserList { get; set; }
        [JsonProperty]
        public bool HideDeletedMessages { get; set; }
        [JsonProperty]
        public bool TrackWhispererNumber { get; set; }
        [JsonProperty]
        public bool AllowCommandWhispering { get; set; }
        [JsonProperty]
        public bool IgnoreBotAccountCommands { get; set; }
        [JsonProperty]
        public bool CommandsOnlyInYourStream { get; set; }
        [JsonProperty]
        public bool DeleteChatCommandsWhenRun { get; set; }

        [JsonProperty]
        public uint DefaultInteractiveGame { get; set; }
        [JsonProperty]
        public bool PreventUnknownInteractiveUsers { get; set; }
        [JsonProperty]
        public bool PreventSmallerCooldowns { get; set; }
        [JsonProperty]
        public List<InteractiveSharedProjectModel> CustomInteractiveProjectIDs { get; set; }

        [JsonProperty]
        public int RegularUserMinimumHours { get; set; }
        [JsonProperty]
        public List<UserTitleModel> UserTitles { get; set; }

        [JsonProperty]
        public bool GameQueueSubPriority { get; set; }
        [JsonProperty]
        public RequirementViewModel GameQueueRequirements { get; set; } = new RequirementViewModel();

        [JsonProperty]
        public bool QuotesEnabled { get; set; }
        [JsonProperty]
        public string QuotesFormat { get; set; }

        [JsonProperty]
        public int TimerCommandsInterval { get; set; } = 10;
        [JsonProperty]
        public int TimerCommandsMinimumMessages { get; set; } = 10;
        [JsonProperty]
        public bool DisableAllTimers { get; set; }

        [JsonProperty]
        public string GiveawayCommand { get; set; } = "giveaway";
        [JsonProperty]
        public bool GiveawayGawkBoxTrigger { get; set; }
        [JsonProperty]
        public bool GiveawayStreamlabsTrigger { get; set; }
        [JsonProperty]
        public bool GiveawayTiltifyTrigger { get; set; }
        [JsonProperty]
        public bool GiveawayDonationRequiredAmount { get; set; }
        [JsonProperty]
        public double GiveawayDonationAmount { get; set; }
        [JsonProperty]
        public int GiveawayTimer { get; set; } = 1;
        [JsonProperty]
        public int GiveawayMaximumEntries { get; set; } = 1;
        [JsonProperty]
        public RequirementViewModel GiveawayRequirements { get; set; } = new RequirementViewModel();
        [JsonProperty]
        public int GiveawayReminderInterval { get; set; } = 5;
        [JsonProperty]
        public bool GiveawayRequireClaim { get; set; } = true;
        [JsonProperty]
        public bool GiveawayAllowPastWinners { get; set; }
        [JsonProperty]
        public CustomCommand GiveawayUserJoinedCommand { get; set; }
        [JsonProperty]
        public CustomCommand GiveawayWinnerSelectedCommand { get; set; }

        [JsonProperty]
        public bool ModerationUseCommunityFilteredWords { get; set; }

        [JsonProperty]
        public int ModerationFilteredWordsTimeout1MinuteOffenseCount { get; set; }
        [JsonProperty]
        public int ModerationFilteredWordsTimeout5MinuteOffenseCount { get; set; }
        [JsonProperty]
        public MixerRoleEnum ModerationFilteredWordsExcempt { get; set; } = MixerRoleEnum.Mod;
        [JsonProperty]
        public bool ModerationFilteredWordsApplyStrikes { get; set; } = true;

        [JsonProperty]
        public int ModerationCapsBlockCount { get; set; }
        [JsonProperty]
        public bool ModerationCapsBlockIsPercentage { get; set; } = true;
        [JsonProperty]
        public int ModerationPunctuationBlockCount { get; set; }
        [JsonProperty]
        public bool ModerationPunctuationBlockIsPercentage { get; set; } = true;
        [JsonProperty]
        public MixerRoleEnum ModerationChatTextExcempt { get; set; } = MixerRoleEnum.Mod;
        [JsonProperty]
        public bool ModerationChatTextApplyStrikes { get; set; } = true;

        [JsonProperty]
        public bool ModerationBlockLinks { get; set; }
        [JsonProperty]
        public MixerRoleEnum ModerationBlockLinksExcempt { get; set; } = MixerRoleEnum.Mod;
        [JsonProperty]
        public bool ModerationBlockLinksApplyStrikes { get; set; } = true;

        [JsonProperty]
        public ModerationChatInteractiveParticipationEnum ModerationChatInteractiveParticipation { get; set; } = ModerationChatInteractiveParticipationEnum.None;
        [JsonProperty]
        public MixerRoleEnum ModerationChatInteractiveParticipationExcempt { get; set; } = MixerRoleEnum.Mod;

        [JsonProperty]
        public bool ModerationResetStrikesOnLaunch { get; set; }
        [JsonProperty]
        public CustomCommand ModerationStrike1Command { get; set; }
        [JsonProperty]
        public CustomCommand ModerationStrike2Command { get; set; }
        [JsonProperty]
        public CustomCommand ModerationStrike3Command { get; set; }

        [JsonProperty]
        public bool EnableOverlay { get; set; }
        [JsonProperty]
        public Dictionary<string, int> OverlayCustomNameAndPorts { get; set; }
        [JsonProperty]
        public string OverlaySourceName { get; set; }
        [JsonProperty]
        public int OverlayWidgetRefreshTime { get; set; } = 5;

        [JsonProperty]
        public string OBSStudioServerIP { get; set; }
        [JsonProperty]
        public string OBSStudioServerPassword { get; set; }

        [JsonProperty]
        public bool EnableStreamlabsOBSConnection { get; set; }

        [JsonProperty]
        public bool EnableXSplitConnection { get; set; }

        [JsonProperty]
        public bool EnableDeveloperAPI { get; set; }

        [JsonProperty]
        public int TiltifyCampaign { get; set; }

        [JsonProperty]
        public int ExtraLifeTeamID { get; set; }
        [JsonProperty]
        public int ExtraLifeParticipantID { get; set; }
        [JsonProperty]
        public bool ExtraLifeIncludeTeamDonations { get; set; }

        [JsonProperty]
        public string DiscordServer { get; set; }

        [JsonProperty]
        public string PatreonTierMixerSubscriberEquivalent { get; set; }

        [JsonProperty]
        public bool UnlockAllCommands { get; set; }

        [JsonProperty]
        public int ChatFontSize { get; set; } = 13;
        [JsonProperty]
        public bool ChatShowUserJoinLeave { get; set; }
        [JsonProperty]
        public string ChatUserJoinLeaveColorScheme { get; set; } = ColorSchemes.DefaultColorScheme;
        [JsonProperty]
        public bool ChatShowEventAlerts { get; set; }
        [JsonProperty]
        public string ChatEventAlertsColorScheme { get; set; } = ColorSchemes.DefaultColorScheme;
        [JsonProperty]
        public bool ChatShowInteractiveAlerts { get; set; }
        [JsonProperty]
        public string ChatInteractiveAlertsColorScheme { get; set; } = ColorSchemes.DefaultColorScheme;

        [JsonProperty]
        public string NotificationChatMessageSoundFilePath { get; set; }
        [JsonProperty]
        public string NotificationChatTaggedSoundFilePath { get; set; }
        [JsonProperty]
        public string NotificationChatWhisperSoundFilePath { get; set; }
        [JsonProperty]
        public string NotificationServiceConnectSoundFilePath { get; set; }
        [JsonProperty]
        public string NotificationServiceDisconnectSoundFilePath { get; set; }

        [JsonProperty]
        public int MaxMessagesInChat { get; set; } = 100;

        [JsonProperty]
        public bool AutoExportStatistics { get; set; }

        [JsonProperty]
        public List<SerialDeviceModel> SerialDevices { get; set; }

        [JsonProperty]
        public RemoteConnectionAuthenticationTokenModel RemoteHostConnection { get; set; }
        [JsonProperty]
        public List<RemoteConnectionModel> RemoteClientConnections { get; set; }

        [JsonProperty]
        public List<FavoriteGroupModel> FavoriteGroups { get; set; }

        [JsonProperty]
        public HashSet<SongRequestServiceTypeEnum> SongRequestServiceTypes { get; set; }
        [JsonProperty]
        public bool SpotifyAllowExplicit { get; set; }

        [JsonProperty]
        public string DefaultPlaylist { get; set; }

        [JsonProperty]
        public int SongRequestVolume { get; set; } = 100;

        [JsonProperty]
        public Dictionary<uint, JObject> CustomInteractiveSettings { get; set; }

        [JsonProperty]
        public string TelemetryUserId { get; set; }

        [JsonProperty]
        public string SettingsBackupLocation { get; set; }
        [JsonProperty]
        public SettingsBackupRateEnum SettingsBackupRate { get; set; }
        [JsonProperty]
        public DateTimeOffset SettingsLastBackup { get; set; }

        [JsonProperty]
        protected Dictionary<Guid, UserCurrencyViewModel> currenciesInternal { get; set; }
        [JsonProperty]
        protected Dictionary<Guid, UserInventoryViewModel> inventoriesInternal { get; set; }

        [JsonProperty]
        protected Dictionary<string, int> cooldownGroupsInternal { get; set; }

        [JsonProperty]
        protected List<PreMadeChatCommandSettings> preMadeChatCommandSettingsInternal { get; set; }
        [JsonProperty]
        protected List<ChatCommand> chatCommandsInternal { get; set; }
        [JsonProperty]
        protected List<EventCommand> eventCommandsInternal { get; set; }
        [JsonProperty]
        protected List<InteractiveCommand> interactiveCommandsInternal { get; set; }
        [JsonProperty]
        protected List<TimerCommand> timerCommandsInternal { get; set; }
        [JsonProperty]
        protected List<ActionGroupCommand> actionGroupCommandsInternal { get; set; }
        [JsonProperty]
        protected List<GameCommandBase> gameCommandsInternal { get; set; }

        [JsonProperty]
        protected List<UserQuoteViewModel> userQuotesInternal { get; set; }

        [JsonProperty]
        protected List<OverlayWidget> overlayWidgetsInternal { get; set; }

        [JsonProperty]
        protected List<RemoteProfileModel> remoteProfilesInternal { get; set; }
        [JsonProperty]
        protected Dictionary<Guid, RemoteProfileBoardsModel> remoteProfileBoardsInternal { get; set; }

        [JsonProperty]
        protected List<string> filteredWordsInternal { get; set; }
        [JsonProperty]
        protected List<string> bannedWordsInternal { get; set; }

        [JsonProperty]
        protected Dictionary<uint, List<InteractiveUserGroupViewModel>> interactiveUserGroupsInternal { get; set; }
        [JsonProperty]
        [Obsolete]
        internal Dictionary<string, int> interactiveCooldownGroupsInternal { get; set; }

        public DesktopSavableChannelSettings()
        {
            this.CommandGroups = new Dictionary<string, CommandGroupSettings>();
            this.HotKeys = new Dictionary<string, HotKeyConfiguration>();
            this.OverlayCustomNameAndPorts = new Dictionary<string, int>();
            this.CustomInteractiveProjectIDs = new List<InteractiveSharedProjectModel>();
            this.UserTitles = new List<UserTitleModel>();
            this.SerialDevices = new List<SerialDeviceModel>();
            this.RemoteClientConnections = new List<RemoteConnectionModel>();
            this.FavoriteGroups = new List<FavoriteGroupModel>();
            this.SongRequestServiceTypes = new HashSet<SongRequestServiceTypeEnum>();
            this.CustomInteractiveSettings = new Dictionary<uint, JObject>();

            this.currenciesInternal = new Dictionary<Guid, UserCurrencyViewModel>();
            this.inventoriesInternal = new Dictionary<Guid, UserInventoryViewModel>();
            this.preMadeChatCommandSettingsInternal = new List<PreMadeChatCommandSettings>();
            this.cooldownGroupsInternal = new Dictionary<string, int>();
            this.chatCommandsInternal = new List<ChatCommand>();
            this.eventCommandsInternal = new List<EventCommand>();
            this.interactiveCommandsInternal = new List<InteractiveCommand>();
            this.timerCommandsInternal = new List<TimerCommand>();
            this.actionGroupCommandsInternal = new List<ActionGroupCommand>();
            this.gameCommandsInternal = new List<GameCommandBase>();
            this.userQuotesInternal = new List<UserQuoteViewModel>();
            this.remoteProfilesInternal = new List<RemoteProfileModel>();
            this.remoteProfileBoardsInternal = new Dictionary<Guid, RemoteProfileBoardsModel>();
            this.overlayWidgetsInternal = new List<OverlayWidget>();
            this.filteredWordsInternal = new List<string>();
            this.bannedWordsInternal = new List<string>();
            this.interactiveUserGroupsInternal = new Dictionary<uint, List<InteractiveUserGroupViewModel>>();
#pragma warning disable CS0612 // Type or member is obsolete
            this.interactiveCooldownGroupsInternal = new Dictionary<string, int>();
#pragma warning restore CS0612 // Type or member is obsolete
        }
    }

    [DataContract]
    public class DesktopChannelSettings : DesktopSavableChannelSettings, IChannelSettings
    {
        private const string CommunityFilteredWordsFilePath = "Assets\\CommunityBannedWords.txt";

        [JsonIgnore]
        public DatabaseDictionary<uint, UserDataViewModel> UserData { get; set; }

        [JsonIgnore]
        public LockedDictionary<Guid, UserCurrencyViewModel> Currencies { get; set; }
        [JsonIgnore]
        public LockedDictionary<Guid, UserInventoryViewModel> Inventories { get; set; }

        [JsonIgnore]
        public LockedDictionary<string, int> CooldownGroups { get; set; }

        [JsonIgnore]
        public LockedList<PreMadeChatCommandSettings> PreMadeChatCommandSettings { get; set; }
        [JsonIgnore]
        public LockedList<ChatCommand> ChatCommands { get; set; }
        [JsonIgnore]
        public LockedList<EventCommand> EventCommands { get; set; }
        [JsonIgnore]
        public LockedList<InteractiveCommand> InteractiveCommands { get; set; }
        [JsonIgnore]
        public LockedList<TimerCommand> TimerCommands { get; set; }
        [JsonIgnore]
        public LockedList<ActionGroupCommand> ActionGroupCommands { get; set; }
        [JsonIgnore]
        public LockedList<GameCommandBase> GameCommands { get; set; }

        [JsonIgnore]
        public LockedList<UserQuoteViewModel> UserQuotes { get; set; }

        [JsonIgnore]
        public LockedList<OverlayWidget> OverlayWidgets { get; set; }

        [JsonIgnore]
        public LockedList<RemoteProfileModel> RemoteProfiles { get; set; }
        [JsonIgnore]
        public LockedDictionary<Guid, RemoteProfileBoardsModel> RemoteProfileBoards { get; set; }

        [JsonIgnore]
        public LockedList<string> FilteredWords { get; set; }
        [JsonIgnore]
        public LockedList<string> BannedWords { get; set; }
        [JsonIgnore]
        public LockedList<string> CommunityFilteredWords { get; set; }

        [JsonIgnore]
        public LockedDictionary<uint, List<InteractiveUserGroupViewModel>> InteractiveUserGroups { get; set; }

        [JsonIgnore]
        public string DatabasePath { get; set; }

        [JsonIgnore]
        internal SQLiteDatabaseWrapper DatabaseWrapper;

        [JsonIgnore]
        internal bool InitializeDB = true;

        public DesktopChannelSettings(ExpandedChannelModel channel, bool isStreamer = true)
            : this()
        {
            this.Channel = channel;
            this.IsStreamer = isStreamer;

            this.GiveawayUserJoinedCommand = CustomCommand.BasicChatCommand("Giveaway User Joined", "You have been entered into the giveaway, stay tuned to see who wins!", isWhisper: true);
            this.GiveawayWinnerSelectedCommand = CustomCommand.BasicChatCommand("Giveaway Winner Selected", "Congratulations @$username, you won! Type \"!claim\" in chat in the next 60 seconds to claim your prize!", isWhisper: true);

            this.ModerationStrike1Command = CustomCommand.BasicChatCommand("Moderation Strike 1", "$moderationreason. You have received a moderation strike & currently have $usermoderationstrikes strike(s)", isWhisper: true);
            this.ModerationStrike2Command = CustomCommand.BasicChatCommand("Moderation Strike 2", "$moderationreason. You have received a moderation strike & currently have $usermoderationstrikes strike(s)", isWhisper: true);
            this.ModerationStrike3Command = CustomCommand.BasicChatCommand("Moderation Strike 3", "$moderationreason. You have received a moderation strike & currently have $usermoderationstrikes strike(s)", isWhisper: true);
        }

        public DesktopChannelSettings()
            : base()
        {
            this.UserData = new DatabaseDictionary<uint, UserDataViewModel>();
            this.Currencies = new LockedDictionary<Guid, UserCurrencyViewModel>();
            this.Inventories = new LockedDictionary<Guid, UserInventoryViewModel>();
            this.CooldownGroups = new LockedDictionary<string, int>();
            this.PreMadeChatCommandSettings = new LockedList<PreMadeChatCommandSettings>();
            this.ChatCommands = new LockedList<ChatCommand>();
            this.EventCommands = new LockedList<EventCommand>();
            this.InteractiveCommands = new LockedList<InteractiveCommand>();
            this.TimerCommands = new LockedList<TimerCommand>();
            this.ActionGroupCommands = new LockedList<ActionGroupCommand>();
            this.GameCommands = new LockedList<GameCommandBase>();
            this.UserQuotes = new LockedList<UserQuoteViewModel>();
            this.RemoteProfiles = new LockedList<RemoteProfileModel>();
            this.OverlayWidgets = new LockedList<OverlayWidget>();
            this.FilteredWords = new LockedList<string>();
            this.BannedWords = new LockedList<string>();
            this.CommunityFilteredWords = new LockedList<string>();
            this.InteractiveUserGroups = new LockedDictionary<uint, List<InteractiveUserGroupViewModel>>();
        }

        public async Task Initialize()
        {
            this.Currencies = new LockedDictionary<Guid, UserCurrencyViewModel>(this.currenciesInternal);
            this.Inventories = new LockedDictionary<Guid, UserInventoryViewModel>(this.inventoriesInternal);
            this.PreMadeChatCommandSettings = new LockedList<PreMadeChatCommandSettings>(this.preMadeChatCommandSettingsInternal);
            this.CooldownGroups = new LockedDictionary<string, int>(this.cooldownGroupsInternal);
            this.ChatCommands = new LockedList<ChatCommand>(this.chatCommandsInternal);
            this.EventCommands = new LockedList<EventCommand>(this.eventCommandsInternal);
            this.InteractiveCommands = new LockedList<InteractiveCommand>(this.interactiveCommandsInternal);
            this.TimerCommands = new LockedList<TimerCommand>(this.timerCommandsInternal);
            this.ActionGroupCommands = new LockedList<ActionGroupCommand>(this.actionGroupCommandsInternal);
            this.GameCommands = new LockedList<GameCommandBase>(this.gameCommandsInternal);
            this.UserQuotes = new LockedList<UserQuoteViewModel>(this.userQuotesInternal);
            this.RemoteProfiles = new LockedList<RemoteProfileModel>(this.remoteProfilesInternal);
            this.RemoteProfileBoards = new LockedDictionary<Guid, RemoteProfileBoardsModel>(this.remoteProfileBoardsInternal);
            this.OverlayWidgets = new LockedList<OverlayWidget>(this.overlayWidgetsInternal);
            this.FilteredWords = new LockedList<string>(this.filteredWordsInternal);
            this.BannedWords = new LockedList<string>(this.bannedWordsInternal);
            this.InteractiveUserGroups = new LockedDictionary<uint, List<InteractiveUserGroupViewModel>>(this.interactiveUserGroupsInternal);

            if (File.Exists(DesktopChannelSettings.CommunityFilteredWordsFilePath))
            {
                this.CommunityFilteredWords = new LockedList<string>(File.ReadAllLines(DesktopChannelSettings.CommunityFilteredWordsFilePath));
            }

            if (this.IsStreamer)
            {
                this.DatabaseWrapper = new SQLiteDatabaseWrapper(this.DatabasePath);
                if (this.InitializeDB)
                {
                    Dictionary<uint, UserDataViewModel> initialUsers = new Dictionary<uint, UserDataViewModel>();
                    await this.DatabaseWrapper.RunReadCommand("SELECT * FROM Users", (SQLiteDataReader dataReader) =>
                    {
                        UserDataViewModel userData = new UserDataViewModel(dataReader, this);
                        initialUsers[userData.ID] = userData;
                    });
                    this.UserData = new DatabaseDictionary<uint, UserDataViewModel>(initialUsers);
                }
            }

            if (string.IsNullOrEmpty(this.TelemetryUserId))
            {
                if (MixItUp.Base.Util.Logger.IsDebug)
                {
                    this.TelemetryUserId = "MixItUpDebuggingUser";
                }
                else
                {
                    this.TelemetryUserId = Guid.NewGuid().ToString();
                }
            }
        }

        public async Task CopyLatestValues()
        {
            this.Version = DesktopChannelSettings.LatestVersion;

            if (ChannelSession.Connection != null)
            {
                this.OAuthToken = ChannelSession.Connection.Connection.GetOAuthTokenCopy();
            }
            if (ChannelSession.BotConnection != null)
            {
                this.BotOAuthToken = ChannelSession.BotConnection.Connection.GetOAuthTokenCopy();
            }

            if (ChannelSession.Services.Streamlabs != null)
            {
                this.StreamlabsOAuthToken = ChannelSession.Services.Streamlabs.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.GawkBox != null)
            {
                this.GawkBoxOAuthToken = ChannelSession.Services.GawkBox.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.Twitter != null)
            {
                this.TwitterOAuthToken = ChannelSession.Services.Twitter.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.Spotify != null)
            {
                this.SpotifyOAuthToken = ChannelSession.Services.Spotify.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.Discord != null)
            {
                this.DiscordOAuthToken = ChannelSession.Services.Discord.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.Tiltify != null)
            {
                this.TiltifyOAuthToken = ChannelSession.Services.Tiltify.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.TipeeeStream != null)
            {
                this.TipeeeStreamOAuthToken = ChannelSession.Services.TipeeeStream.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.TreatStream != null)
            {
                this.TreatStreamOAuthToken = ChannelSession.Services.TreatStream.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.StreamJar != null)
            {
                this.StreamJarOAuthToken = ChannelSession.Services.StreamJar.GetOAuthTokenCopy();
            }
            if (ChannelSession.Services.Patreon != null)
            {
                this.PatreonOAuthToken = ChannelSession.Services.Patreon.GetOAuthTokenCopy();
            }

            this.currenciesInternal = this.Currencies.ToDictionary();
            this.inventoriesInternal = this.Inventories.ToDictionary();
            this.preMadeChatCommandSettingsInternal = this.PreMadeChatCommandSettings.ToList();
            this.cooldownGroupsInternal = this.CooldownGroups.ToDictionary();
            this.chatCommandsInternal = this.ChatCommands.ToList();
            this.eventCommandsInternal = this.EventCommands.ToList();
            this.interactiveCommandsInternal = this.InteractiveCommands.ToList();
            this.timerCommandsInternal = this.TimerCommands.ToList();
            this.actionGroupCommandsInternal = this.ActionGroupCommands.ToList();
            this.gameCommandsInternal = this.GameCommands.ToList();
            this.userQuotesInternal = this.UserQuotes.ToList();
            this.remoteProfilesInternal = this.RemoteProfiles.ToList();
            this.remoteProfileBoardsInternal = this.RemoteProfileBoards.ToDictionary();
            this.overlayWidgetsInternal = this.OverlayWidgets.ToList();
            this.filteredWordsInternal = this.FilteredWords.ToList();
            this.bannedWordsInternal = this.BannedWords.ToList();
            this.interactiveUserGroupsInternal = this.InteractiveUserGroups.ToDictionary();

            if (this.IsStreamer)
            {
                IEnumerable<uint> removedUsers = this.UserData.GetRemovedValues();
                await this.DatabaseWrapper.RunBulkWriteCommand("DELETE FROM Users WHERE ID = @ID", removedUsers.Select(u => new List<SQLiteParameter>() { new SQLiteParameter("@ID", value: (int)u) }));

                IEnumerable<UserDataViewModel> addedUsers = this.UserData.GetAddedValues();
                addedUsers = addedUsers.Where(u => !string.IsNullOrEmpty(u.UserName));
                await this.DatabaseWrapper.RunBulkWriteCommand("INSERT INTO Users(ID, UserName, ViewingMinutes, CurrencyAmounts, InventoryAmounts, CustomCommands, Options) VALUES(?,?,?,?,?,?,?)",
                    addedUsers.Select(u => new List<SQLiteParameter>() { new SQLiteParameter(DbType.UInt32, u.ID), new SQLiteParameter(DbType.String, value: u.UserName),
                    new SQLiteParameter(DbType.Int32, value: u.ViewingMinutes), new SQLiteParameter(DbType.String, value: u.GetCurrencyAmountsString()),
                    new SQLiteParameter(DbType.String, value: u.GetInventoryAmountsString()), new SQLiteParameter(DbType.String, value: u.GetCustomCommandsString()),
                    new SQLiteParameter(DbType.String, value: u.GetOptionsString()) }));

                IEnumerable<UserDataViewModel> changedUsers = this.UserData.GetChangedValues();
                changedUsers = changedUsers.Where(u => !string.IsNullOrEmpty(u.UserName));
                await this.DatabaseWrapper.RunBulkWriteCommand("UPDATE Users SET UserName = @UserName, ViewingMinutes = @ViewingMinutes, CurrencyAmounts = @CurrencyAmounts," +
                    " InventoryAmounts = @InventoryAmounts, CustomCommands = @CustomCommands, Options = @Options WHERE ID = @ID",
                    changedUsers.Select(u => new List<SQLiteParameter>() { new SQLiteParameter("@UserName", value: u.UserName), new SQLiteParameter("@ViewingMinutes", value: u.ViewingMinutes),
                    new SQLiteParameter("@CurrencyAmounts", value: u.GetCurrencyAmountsString()), new SQLiteParameter("@InventoryAmounts", value: u.GetInventoryAmountsString()),
                    new SQLiteParameter("@CustomCommands", value: u.GetCustomCommandsString()), new SQLiteParameter("@Options", value: u.GetOptionsString()), new SQLiteParameter("@ID", value: (int)u.ID) }));
            }
        }

        public Version GetLatestVersion() { return Assembly.GetEntryAssembly().GetName().Version; }
    }
}
