try {
    dnnsf.api.tabspro.changeTab({
        mid: context.TabsProModuleID,
        tabId: context.TabId,
        refresh: context.Refresh,
        qs: context.Qs
    });
} catch (e) {
    console.error('Failed to change TabsPro module #' + TabsProModuleID + ' tab to tab with id #' + TabId, e);
}